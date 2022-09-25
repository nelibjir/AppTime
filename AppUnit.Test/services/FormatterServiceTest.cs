using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moq;
using NUnit.Framework;
using AppTime.Dtos;
using AppTime.Services;

namespace AppTime.Test.services
{
  [TestFixture]
  public class FormatterServiceTest: GenericTest<DataFileDto[]>
  {

        private const string _JSON = "[{\r\n  \"id\": 1,\r\n  \"first_name\": \"Ranna\",\r\n  \"last_name\": \"Deveraux\",\r\n  \"email\": \"rdeveraux0@ed.gov\",\r\n  \"ip_address\": \"25.226.166.160\"\r\n}, {\r\n  \"id\": 2,\r\n  \"first_name\": \"Mordy\",\r\n  \"last_name\": \"Springer\",\r\n  \"email\": \"mspringer1@about.me\",\r\n  \"ip_address\": \"151.238.47.94\"\r\n}, {\r\n  \"id\": 3,\r\n  \"first_name\": \"Polly\",\r\n  \"last_name\": \"Dark\",\r\n  \"email\": \"pdark2@live.com\",\r\n  \"ip_address\": \"27.42.11.236\"\r\n}, {\r\n  \"id\": 4,\r\n  \"first_name\": \"Vittorio\",\r\n  \"last_name\": \"Peaddie\",\r\n  \"email\": \"vpeaddie3@tripod.com\",\r\n  \"ip_address\": \"109.134.81.253\"\r\n}, {\r\n  \"id\": 5,\r\n  \"first_name\": \"Hillery\",\r\n  \"last_name\": \"Easson\",\r\n  \"email\": \"heasson4@artisteer.com\",\r\n  \"ip_address\": \"252.212.80.76\"\r\n}, {\r\n  \"id\": 6,\r\n  \"first_name\": \"Nadeen\",\r\n  \"last_name\": \"Geldeford\",\r\n  \"email\": \"ngeldeford5@fema.gov\",\r\n  \"ip_address\": \"0.167.252.15\"\r\n}, {\r\n  \"id\": 7,\r\n  \"first_name\": \"Malissia\",\r\n  \"last_name\": \"Ekless\",\r\n  \"email\": \"mekless6@ehow.com\",\r\n  \"ip_address\": \"197.227.55.78\"\r\n}, {\r\n  \"id\": 8,\r\n  \"first_name\": \"Samuele\",\r\n  \"last_name\": \"Woollam\",\r\n  \"email\": \"swoollam7@feedburner.com\",\r\n  \"ip_address\": \"251.194.59.128\"\r\n}, {\r\n  \"id\": 9,\r\n  \"first_name\": \"Simon\",\r\n  \"last_name\": \"Lesor\",\r\n  \"email\": \"slesor8@google.com.br\",\r\n  \"ip_address\": \"208.145.8.106\"\r\n}, {\r\n  \"id\": 10,\r\n  \"first_name\": \"Druci\",\r\n  \"last_name\": \"Borsay\",\r\n  \"email\": \"dborsay9@bandcamp.com\",\r\n  \"ip_address\": \"247.183.34.230\"\r\n}]";
        private const string _XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<root>\r\n   <element>\r\n      <email>aleece0@hugedomains.com</email>\r\n      <first_name>Alexis</first_name>\r\n      <id>1</id>\r\n      <ip_address>106.162.224.191</ip_address>\r\n      <last_name>Leece</last_name>\r\n   </element>\r\n   <element>\r\n      <email>nmccosh1@wisc.edu</email>\r\n      <first_name>Natale</first_name>\r\n      <id>2</id>\r\n      <ip_address>153.186.107.203</ip_address>\r\n      <last_name>McCosh</last_name>\r\n   </element>\r\n   <element>\r\n      <email>vgeorgins2@godaddy.com</email>\r\n      <first_name>Vin</first_name>\r\n      <id>3</id>\r\n      <ip_address>197.107.253.14</ip_address>\r\n      <last_name>Georgins</last_name>\r\n   </element>\r\n   <element>\r\n      <email>bduiged3@yelp.com</email>\r\n      <first_name>Burch</first_name>\r\n      <id>4</id>\r\n      <ip_address>6.36.40.173</ip_address>\r\n      <last_name>Duiged</last_name>\r\n   </element>\r\n   <element>\r\n      <email>bchecklin4@ucsd.edu</email>\r\n      <first_name>Brendan</first_name>\r\n      <id>5</id>\r\n      <ip_address>120.206.234.240</ip_address>\r\n      <last_name>Checklin</last_name>\r\n   </element>\r\n   <element>\r\n      <email>gtitchard5@opera.com</email>\r\n      <first_name>Glenn</first_name>\r\n      <id>6</id>\r\n      <ip_address>97.255.190.43</ip_address>\r\n      <last_name>Titchard</last_name>\r\n   </element>\r\n   <element>\r\n      <email>acumo6@forbes.com</email>\r\n      <first_name>Andros</first_name>\r\n      <id>7</id>\r\n      <ip_address>182.117.213.232</ip_address>\r\n      <last_name>Cumo</last_name>\r\n   </element>\r\n   <element>\r\n      <email>rbapty7@earthlink.net</email>\r\n      <first_name>Rafi</first_name>\r\n      <id>8</id>\r\n      <ip_address>223.3.215.236</ip_address>\r\n      <last_name>Bapty</last_name>\r\n   </element>\r\n   <element>\r\n      <email>akeson8@google.cn</email>\r\n      <first_name>Artemis</first_name>\r\n      <id>9</id>\r\n      <ip_address>81.164.180.16</ip_address>\r\n      <last_name>Keson</last_name>\r\n   </element>\r\n   <element>\r\n      <email>kgogan9@ask.com</email>\r\n      <first_name>Keeley</first_name>\r\n      <id>10</id>\r\n      <ip_address>8.117.215.59</ip_address>\r\n      <last_name>Gogan</last_name>\r\n   </element>\r\n</root>";

	[Test]
    public void CreateJsonFromXmlTest()
    {

      Mock<IJsonFormatterService> formatService = new Mock<IJsonFormatterService>(MockBehavior.Strict);
	  formatService.Setup(f => f.CreateJsonFromXml(_JSON)).Returns(_XML);

	  formatService.VerifyAll();
    }

    [Test]
    public void ParseDatasetTest()
    {
    // TODO MOCK DB and test getting files from DB
        }
  }
}
