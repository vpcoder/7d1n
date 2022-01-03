using NLog;
using System;
using System.Web.Http;
using com.baensi.sdon.protocol;
using com.baensi.sdon.protocol.entities;
using System.Diagnostics;

namespace com.baensi.sdon.server.rest
{

    /// <summary>
    /// Базовый класс контроллера
    /// </summary>
    public abstract class ControllerBase : ApiController
    {

        #region Hidden Fields

        /// <summary>
        /// Логгер, через который можно логировать текущее состояние
        /// </summary>
        protected ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Ctors

        public ControllerBase()
        {

        }

        #endregion

        #region Handler Methods

        /// <summary>
        /// **NOEXCEPT**
        /// Безопасная обработка внутри лямбды, лямбда возвращает сериализованную модель типа T
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения</typeparam>
        /// <param name="callback">Вызываемый каллбек (то что нужно сделать в рамках безопасной лямбды), он должен вернуть модельку типа T</param>
        /// <param name="exceptionCallback"></param>
        /// <returns>Лямбда для перехвата исключения</returns>
        /// <example>
        /// Пример использования:
        /// <code><![CDATA[
        /// 
        ///     var json = "{ 'name':'test', 'data':'text' }";
        /// 
        ///     class MyModel : TransportEntity
        ///     {
        ///         public string name { get; set; }
        ///         public string data { get; set; }
        ///     }
        /// 
        ///     public MyModel ServiceMethod(MyInputClass input)
        ///     {
        ///         return TryT<MyModel>(() =>
        ///         {
        ///             var data = GetSomethingData();
        ///             var name = GetSomethingName();
        ///             
        ///             return new MyModel() { data = data, name = name };
        ///         });
        ///     }
        /// 
        /// ]]></code>
        /// </example>
        protected T TryT<T>(Func<T> callback, Action<Exception> exceptionCallback = null)
            where T : TransportEntity, new()
        {

			#region Debug Section

			var stackTrace = new StackTrace();
			var frame = stackTrace.GetFrame(1);

			logger.Info("called: " + frame.GetMethod().Name);

			#endregion

			T result = null;

            try
            {
                result = callback();
            }
            catch (Exception ex)
            {
                logger.Error(ex);

                try
                {
                    exceptionCallback?.Invoke(ex);
                }
                catch (Exception ex2)
                {
                    logger.Error(ex2);
                }

                result = new T();
                result.ex = ex.ToData();
            }

            return result;
        }

        /// <summary>
        /// **NOEXCEPT**
        /// Безопасная обработка внутри лямбды, лямбда делает некоторое действие, если оно совершилось без ошибок, ex будет = null
        /// </summary>
        /// <param name="callback">Вызываемый каллбек (то что нужно сделать в рамках безопасной лямбды)</param>
        /// <param name="exceptionCallback"></param>
        /// <returns>Лямбда для перехвата исключения</returns>
        /// <example>
        /// Пример использования:
        /// <code><![CDATA[
        /// 
        ///     public VoidResult ServiceMethod(MyInputClass input)
        ///     {
        ///         return Try(() =>
        ///         {
        ///             DoSomething(input);
        ///         });
        ///     }
        /// 
        /// ]]></code>
        /// </example>
        protected VoidResult Try(Action callback, Action<Exception> exceptionCallback = null)
        {
            var result = new VoidResult();

            try
            {
                callback();
            }
            catch (Exception ex)
            {
                logger.Error(ex);

                try
                {
                    exceptionCallback?.Invoke(ex);
                }
                catch (Exception ex2)
                {
                    logger.Error(ex2);
                }

                result.ex = ex.ToData();
            }

            return result;
        }

        #endregion

    }

}
