using MotorBikeRental.Data;
using MotorBikeRental.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using MotorBikeRental.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MotorBikeRental.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly DataContext _context = new DataContext();

        public bool Add(Motorcycle motorcycle)
        {
            try
            {
                _context.Motorcycles.Add(motorcycle);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Motorcycle motorcycle)
        {
            try
            {
                var motorcycleUpdate = _context.Motorcycles.FirstOrDefault(x => x.ID == motorcycle.ID);

                if (motorcycle.Ano != null)
                    motorcycleUpdate.Ano = motorcycle.Ano;
                if (motorcycle.Modelo != null && motorcycle.Modelo != "")
                    motorcycleUpdate.Modelo = motorcycle.Modelo;
                if (motorcycle.Placa != null && motorcycle.Placa != "")
                    motorcycleUpdate.Placa = motorcycle.Placa;
                if(motorcycle.Statulocacao != null && motorcycle.Statulocacao <= 1)
                    motorcycleUpdate.Statulocacao = motorcycle.Statulocacao;

                _context.Motorcycles.Update(motorcycleUpdate);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var motorcycle = _context.Motorcycles.FirstOrDefault(x => x.ID == id);
                _context.Motorcycles.Remove(motorcycle);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Motorcycle> GetList()
        {
            try
            {
                var motorcycles = _context.Motorcycles.AsNoTracking().OrderBy(x => x.ID);

                return motorcycles.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Motorcycle GetById(int id)
        {
            try
            {
                var motorcycle = _context.Motorcycles.FirstOrDefault(x => x.ID == id);

                return motorcycle;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Motorcycle GetByPlate(string plate)
        {
            try
            {
                var motorcycle = _context.Motorcycles.FirstOrDefault(x => x.Placa == plate);

                return motorcycle;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Motorcycle GetMotorcycleAvailable()
        {
            try
            {
                var motorcycle = _context.Motorcycles.FirstOrDefault(x => x.Statulocacao == 0);

                return motorcycle;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
