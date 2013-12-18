//This only works when contour is installed.
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Umbraco.Forms.CodeFirst;
//using Umbraco.Forms.Core.Providers.FieldTypes;

//namespace Example.Forms
//{
//	[Form("Contact", 
//		Id = "d1ef430a-e418-4552-8afe-242549f8aa34", 
//		DisableDefaultStylesheet = true,
//		RequiredErrorMessage= "*", // of "{0} is verplicht",
//		InvalidErrorMessage= "*", // of "{0} is niet geldig"
//		MessageOnSubmit="Bedankt, wij nemen spoedig contact met u op")]
//	public class Contact : FormBase
//	{
//		[Field("Contact", "Main", Mandatory = true)]
//		public string Naam { get; set; } //verplicht

//		[Field("Contact", "Main", Mandatory = true)]
//		public string Bedrijfsnaam { get; set; }

//		[Field("Contact", "Main", Mandatory = true, Regex=@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", Caption="e-mailadres")]
//		public string Emailadres { get; set; } //verplicht

//		[Field("Contact", "Main", Mandatory = false)]
//		public string Telefoonnummer { get; set; } //niet verplicht

//		[Field("Contact", "Main", Mandatory = true, Type = typeof(Textarea))]
//		public string Bericht { get; set; } //verplicht, multiline

//	}
//}