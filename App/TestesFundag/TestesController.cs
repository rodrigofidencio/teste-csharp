using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fundagMVC.Models;
using NUnit.Framework;
using fundagMVC.Controllers;
using Assert = NUnit.Framework.Assert;

namespace TestesFundag
{
	[TestClass]
	public class TestesController
	{
		[TestMethod]
		public void TesteIndex()
		{
			var Controller = new AdiantamentoController();
			var result = controller.Index(2) ;
			Assert.AreEqual("Details", result);

		}
	}
}
