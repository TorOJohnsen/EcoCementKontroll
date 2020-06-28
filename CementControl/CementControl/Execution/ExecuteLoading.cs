using System;
using System.Collections.Generic;
using CementControl.DataAccess;
using CementControl.Models;
using Serilog;

namespace CementControl.Execution
{
    public class ExecuteLoading
    {

        private readonly CementDataServices _dbServices;
        private readonly HandlerRs232WeigthScale _handlerRs232WeigthScale;
        private readonly HandlerRs232PowerSupply _handlerRs232PowerSupply;

        private readonly ILogger _logger = Log.ForContext<ExecuteLoading>();

        private double _cementLoadGoal = 0.0;
        private double _currentSiloWeight = 0.0;
        private double _cementLoaded = 0.0;
        private double _currentVoltage = 0.0;
        private double _startingWeigth = 0.0;
        private double _averageWeight = 0.0;

        private readonly Buffer<double> _avgBuffer;
        private readonly int _numReadingsForAverage = 2;



        private ExecutionState _executionState;
        private string _description = "Init";

        public event EventHandler CementLoadFinished;


        public ExecuteLoading(CementDataServices dbServices, HandlerRs232WeigthScale handlerRs232WeigthScale, HandlerRs232PowerSupply handlerRs232PowerSupply)
        {
            _dbServices = dbServices;
            _handlerRs232WeigthScale = handlerRs232WeigthScale;
            _handlerRs232PowerSupply = handlerRs232PowerSupply;
            _avgBuffer = new Buffer<double>(_numReadingsForAverage);
            _executionState = ExecutionState.Init;

            _logger.Information($"Average taken over {_numReadingsForAverage} readings");
        }


        public void StartLoading(double cementToBeLoaded, string description)
        {

            _logger.Information("Cement loading starting");

            // Update variables
            _cementLoadGoal = cementToBeLoaded;
            //_startingWeigth = _currentSiloWeight;
            _startingWeigth = _averageWeight;
            _description = description;
            _cementLoaded = 0.0;

            // Turn on silo loader
            _handlerRs232PowerSupply.TurnOn();
            //_handlerRs232PowerSupply.GetVoltage();

            // Set execution state
            _executionState = ExecutionState.Loading;
        }

        private void LoadingFinished()
        {
            _logger.Information("Cement loading finished");

            // Turn on silo loader
            TurnOffSiloLoader();

            // Set execution state
            _executionState = ExecutionState.FinishedLoading;

            OnCementLoadFinished();
        }


        private bool IsFinishedLoading()
        {
            return _cementLoaded >= _cementLoadGoal;
        }

        
        public void Execution()
        {
            if (_executionState == ExecutionState.Loading)
            {
                //_cementLoaded = _startingWeigth -_currentSiloWeight;
                _cementLoaded = _startingWeigth -_averageWeight;

                if (IsFinishedLoading())
                {
                    LoadingFinished();
                }
            }
        }



        private void TurnOffSiloLoader()
        {
            // Turn on silo loader
            _handlerRs232PowerSupply.TurnOff();
            _handlerRs232PowerSupply.GetVoltage();
        }



        public void StopLoading()
        {

            _logger.Information("Cement loading stopped");

            TurnOffSiloLoader();

            // Set execution state
            _executionState = ExecutionState.StoppedLoading;
            
            OnCementLoadFinished();
        }

        public void SaveData()
        {
            _dbServices.SaveCementData(GenerateCementData());
        }

        public void UpdateCurrentWeight(double weight)
        {
            _logger.Debug($"CurrentWeight: {weight:F1}kg");
            _currentSiloWeight = weight;
            _avgBuffer.Add(weight);
            _averageWeight = Average(_avgBuffer);
        }

        public void UpdateVoltageSetting(double voltage)
        {
            _logger.Debug($"Voltage: {voltage:F1}V");
            _currentVoltage = voltage;
        }


        public double GetCurrentlyLoadedCement()
        {
            return _cementLoaded;
        }


        protected virtual void OnCementLoadFinished()
        {
            _logger.Information("Cement loading finished event");
            CementLoadFinished?.Invoke(this, EventArgs.Empty);

        }


        internal CementData GenerateCementData()
        {
            return  (new CementData(_description,_currentSiloWeight, _averageWeight, _executionState,_currentVoltage, _cementLoadGoal, _cementLoaded, _startingWeigth));
        }


        private double Average(Buffer<double> b)
        {
            double sum = 0.0;
            foreach (var d in b)
            {
                sum += d;
            }

            if (b.Count == 0)
            {
                return 0.0;
            }

            return sum / b.Count;
        }




    }



    public enum ExecutionState
    {
        Init,
        Loading,
        StoppedLoading,
        FinishedLoading
    }


    public class Buffer<T> : Queue<T>
    {
        private int? maxCapacity { get; set; }

        public Buffer() { maxCapacity = null; }
        public Buffer(int capacity) { maxCapacity = capacity; }

        public void Add(T newElement)
        {
            if (this.Count == (maxCapacity ?? -1)) this.Dequeue(); // no limit if maxCapacity = null
            this.Enqueue(newElement);
        }
    }

}
