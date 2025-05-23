﻿using MeetingManager.Domain.Exceptions;
using MeetingManager.Domain.Interfaces.Repositories;
using MeetingManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MeetingManager.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(ApplicationDbContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IQueryable<T> GetDbSet()
        {
            return _dbSet;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Сущность {typeof(T).Name} успешно добавлена.");
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Ошибка при добавлении сущности {typeof(T).Name}.");
                throw new DataBaseException($"Ошибка при добавлении записи  {typeof(T).Name} в базу данных.", ex);
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Сущность {typeof(T).Name} успешно обновлена.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении сущности {typeof(T).Name} .");
                throw new DataBaseException($"Ошибка при обновлении записи {typeof(T).Name} в базе данных.", ex);
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Сущность {typeof(T).Name} успешно удалена.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении сущности {typeof(T).Name} .");
                throw new DataBaseException($"Ошибка при удалении записи  {typeof(T).Name} из базы данных.", ex);
            }
        }

        public async Task<List<T>> GetAllAsNoTrackingToListAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}
