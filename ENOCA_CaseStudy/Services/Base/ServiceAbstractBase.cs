﻿using AutoMapper;
using ENOCA_CaseStudy.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ENOCA_CaseStudy.Services.Base
{
    public abstract class ServiceAbstractBase<TEntity, TModel> : IServiceBase<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ServiceAbstractBase(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<TModel> Add(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            var addedRecord = await _db.Set<TEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
            var returndata = _mapper.Map<TModel>(addedRecord.Entity);
            return returndata;
        }

        public async Task<string> Delete(int id)
        {
            var entity = await _db.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _db.Set<TEntity>().Remove(entity);
                await _db.SaveChangesAsync();
                return "Veri Silindi";
            }
            return "Veri silinirken bir hata oluştu.";
        }

        public async Task<IEnumerable<TModel>> Get(Expression<Func<TModel, bool>> predicate = null)
        {
            try
            {
                if (predicate != null)
                {
                    var visitor = new ParameterTypeVisitor<TModel, TEntity>(predicate);
                    var entityLambda = visitor.Convert();
                    var listquery = _db.Set<TEntity>().Where(entityLambda);
                    var modellistquery = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(listquery);
                    return modellistquery.ToList();
                }
                else
                {
                    var listquery = _db.Set<TEntity>();
                    var modellistquery = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(listquery);
                    return modellistquery.ToList();
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<TModel> GetById(int id)
        {
            try
            {
                var entity = await _db.Set<TEntity>().FindAsync(id);
                var returndata = _mapper.Map<TModel>(entity);
                return returndata;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TModel> Update(TModel model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                var returndata = _mapper.Map<TModel>(entity);
                return returndata;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public class ParameterTypeVisitor<TFrom, TTo> : ExpressionVisitor
        {

            private Dictionary<string, ParameterExpression> convertedParameters;
            private Expression<Func<TFrom, bool>> expression;

            public ParameterTypeVisitor(Expression<Func<TFrom, bool>> expresionToConvert)
            {

                //for each parameter in the original expression creates a new parameter with the same name but with changed type 
                convertedParameters = expresionToConvert.Parameters
                    .ToDictionary(
                        x => x.Name,
                        x => Expression.Parameter(typeof(TTo), x.Name)
                    );

                expression = expresionToConvert;
            }

            public Expression<Func<TTo, bool>> Convert()
            {
                return (Expression<Func<TTo, bool>>)Visit(expression);
            }

            //handles Properties and Fields accessors 
            protected override Expression VisitMember(MemberExpression node)
            {
                //we want to replace only the nodes of type TFrom
                //so we can handle expressions of the form x=> x.Property.SubProperty
                //in the expression x=> x.Property1 == 6 && x.Property2 == 3
                //this replaces         ^^^^^^^^^^^         ^^^^^^^^^^^            
                if (node.Member.DeclaringType == typeof(TFrom))
                {
                    //gets the memberinfo from type TTo that matches the member of type TFrom
                    var memeberInfo = typeof(TTo).GetMember(node.Member.Name).First();

                    //this will actually call the VisitParameter method in this class
                    var newExp = Visit(node.Expression);
                    return Expression.MakeMemberAccess(newExp, memeberInfo);
                }
                else
                {
                    return base.VisitMember(node);
                }
            }

            // this will be called where ever we have a reference to a parameter in the expression
            // for ex. in the expression x=> x.Property1 == 6 && x.Property2 == 3
            // this will be called twice     ^                   ^
            protected override Expression VisitParameter(ParameterExpression node)
            {
                var newParameter = convertedParameters[node.Name];
                return newParameter;
            }

            //this will be the first Visit method to be called
            //since we're converting LamdaExpressions
            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                //visit the body of the lambda, this will Traverse the ExpressionTree 
                //and recursively replace parts of the expression we for which we have matching Visit methods 
                var newExp = Visit(node.Body);

                //this will create the new expression            
                return Expression.Lambda(newExp, convertedParameters.Select(x => x.Value));
            }
        }
    }
}
