﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccine.Domain.Domain;
using Vaccine.Repository.Interface;

namespace Vaccine.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        //string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T).IsAssignableFrom(typeof(Vaccines)))
            {
                return entities
                    .Include("PatientFor")
                    .Include("Center")
                    .AsEnumerable();
            }
            else if (typeof(T).IsAssignableFrom(typeof(VaccinationCenter)))
            {
                return entities
                    .Include("Vaccines")
                    .Include("Vaccines.PatientFor")
                    .AsEnumerable();
            }
            else
            {
                return entities.AsEnumerable();
            }
        }

        public T Get(Guid? id)
        {
            if (typeof(T).IsAssignableFrom(typeof(Vaccines)))
            {
                return entities
                    .Include("PatientFor")
                    .Include("Center")
                    .First(s => s.Id == id);
            }
            else if (typeof(T).IsAssignableFrom(typeof(VaccinationCenter)))
            {
                return entities
                    .Include("Vaccines")
                    .Include("Vaccines.PatientFor")
                    .First(s => s.Id == id);
            }
            else if (typeof(T).IsAssignableFrom(typeof(Patient)))
            {
                return entities
                    .Include("VaccinationSchedule")
                    .Include("VaccinationSchedule.Center")
                    .First(s => s.Id == id);
            }

            else
            {
                return entities.First(s => s.Id == id);
            }

        }
        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
            return entity;
        }

        public List<T> InsertMany(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            entities.AddRange(entities);
            context.SaveChanges();
            return entities;
        }
    }

}
