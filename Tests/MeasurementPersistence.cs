using System;
using System.Threading.Tasks;
using WebAPI.Gateway.Persistence;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;

namespace Tests
{
    public class MeasurementPersistence
    {
        private readonly Account _account;
        private readonly Plant _plant;
        private readonly Measurement _measurement, _measurement1, _measurement2, _measurement3, _measurement4, _measurement5;

        private const string Eui = "qwerty";
        private const string Username = "Pupendo";

        public MeasurementPersistence()
        {
            _account = new Account()
            {
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                Password = "123456",
                Region = "Europe",
                Username = Username
            };

            _plant = new Plant()
            {
                DOB = DateTime.Now,
                EUI = Eui,
                Nickname = "Vladimir Trump"
            };
            
            _measurement = new Measurement()
            {
                CO2 = 550,
                Humidity = 40,
                Light = 69,
                Temperature = (decimal) 21.50,
                TimeStamp = DateTime.Now
            };
            
            _measurement1 = new Measurement()
            {
                CO2 = 626,
                Humidity = 30,
                Light = 34,
                Temperature = (decimal) 18.80,
                TimeStamp = DateTime.Now
            };
            
            _measurement2 = new Measurement()
            {
                CO2 = 450,
                Humidity = 55,
                Light = 189,
                Temperature = (decimal) 25,
                TimeStamp = DateTime.Now
            };
            
            _measurement3 = new Measurement()
            {
                CO2 = 700,
                Humidity = 32,
                Light = 234,
                Temperature = (decimal) 23,
                TimeStamp = DateTime.Now
            };
            
            _measurement4 = new Measurement()
            {
                CO2 = 543,
                Humidity = 34,
                Light = 123,
                Temperature = (decimal) 23.45,
                TimeStamp = DateTime.Now
            };
            
            _measurement5 = new Measurement()
            {
                CO2 = 420,
                Humidity = 45,
                Light = 12,
                Temperature = (decimal) 20,
                TimeStamp = DateTime.Now
            };
        }

        public async Task PersistMeasurement()
        {
            IAccountRepo accountRepo = new AccountRepo();
            await accountRepo.PostAccountAsync(_account);

            IPlantRepo plantRepo = new PlantRepo();
            await plantRepo.PostPlantAsync(_plant,Username);
            
            ILoriotRepo loriotRepo = new LoriotRepo();
            await loriotRepo.AddMeasurement(_measurement, Eui);
            await loriotRepo.AddMeasurement(_measurement1, Eui);
            await loriotRepo.AddMeasurement(_measurement2, Eui);
            await loriotRepo.AddMeasurement(_measurement3, Eui);
            await loriotRepo.AddMeasurement(_measurement4, Eui);
            await loriotRepo.AddMeasurement(_measurement5, Eui);
        }

        public async Task PersistMeasurement(Measurement measurement)
        {
            ILoriotRepo loriotRepo = new LoriotRepo();
            await loriotRepo.AddMeasurement(measurement, Eui);
        }

        public async Task DeleteMeasurement()
        {
            IAccountRepo accountRepo = new AccountRepo();
            await accountRepo.DeleteAccountAsync(Username);
        }
    }
}