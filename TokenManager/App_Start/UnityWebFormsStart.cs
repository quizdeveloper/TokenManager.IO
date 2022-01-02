using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using Microsoft.Practices.Unity;
using TokenManager.Bsl.Token;
using TokenManager.Core.AppSetting;
using TokenManager.Dal.DBHelper;
using TokenManager.Dal.Token;
using Unity.WebForms;

[assembly: WebActivatorEx.PostApplicationStartMethod( typeof(TokenManager.App_Start.UnityWebFormsStart), "PostStart" )]
namespace TokenManager.App_Start
{
	/// <summary>
	///		Startup class for the Unity.WebForms NuGet package.
	/// </summary>
	internal static class UnityWebFormsStart
	{
		/// <summary>
		///     Initializes the unity container when the application starts up.
		/// </summary>
		/// <remarks>
		///		Do not edit this method. Perform any modifications in the
		///		<see cref="RegisterDependencies" /> method.
		/// </remarks>
		internal static void PostStart()
		{
			IUnityContainer container = new UnityContainer();
			HttpContext.Current.Application.SetContainer( container );

			RegisterDependencies( container );
		}

		/// <summary>
		///		Registers dependencies in the supplied container.
		/// </summary>
		/// <param name="container">Instance of the container to populate.</param>
		private static void RegisterDependencies( IUnityContainer container )
		{
			// TODO: Add any dependencies needed here
			Dictionary<string, string> connectionsDic = new Dictionary<string, string>();
			connectionsDic.Add("TokenManagerConnectionString", AppSettingUtils.GetConnection("TokenManagerConnectionString"));

			var db = (SqlConnection)new DbConnectionFactory(connectionsDic).CreateMsSqlConnection("TokenManagerConnectionString");
			container.RegisterInstance<IDbConnection>(db);
			container.RegisterType<ITokenDal, TokenDal>();
			container.RegisterType<ITokenBsl, TokenBsl>();
		}
	}
}