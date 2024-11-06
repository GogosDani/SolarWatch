using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch;
using SolarWatch.Controllers;
using SolarWatch.Services;
using SolarWatch.Services.JsonParsers;

namespace SolarWatchTest;

public class SolarControllerTest
{
    private Mock<ILogger<SolarWatchController>> _mockLogger;
    private Mock<ICityDataProvider> _cityApiReader;
    private Mock<ISolarInfoProvider> _solarInfoReader;
    private Mock<ICityParser> _cityParser;
    private Mock<ISolarParser> _solarParser;
    private SolarWatchController _controller;

    [SetUp]
    public void Setup()
    {
        _cityApiReader = new Mock<ICityDataProvider>();
        _solarInfoReader = new Mock<ISolarInfoProvider>();
        _cityParser = new Mock<ICityParser>();
        _solarParser = new Mock<ISolarParser>();
        _mockLogger = new Mock<ILogger<SolarWatchController>>();
        _controller = new SolarWatchController(_mockLogger.Object, _cityApiReader.Object, _cityParser.Object, _solarInfoReader.Object, _solarParser.Object);
    }

    [Test]
    public void GetSolarInfosReturnsNotFoundWhenCityApiFailed()
    {
        _cityApiReader.Setup(x => x.GetCityData(It.IsAny<string>())).Throws(new Exception());
        var result = _controller.GetSolarInfos("budapest", new DateOnly(2021, 12, 10));
        Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
    }

    [Test]
    public void TestWithCorrectCityNameAndDate()
    {
        var city = new City(12.12, 12.12);
        var solar = new Solar("12:12", "10:10");
        _cityApiReader.Setup(x => x.GetCityData(It.IsAny<string>())).Returns("city");
        _cityParser.Setup(x => x.Process(It.IsAny<string>())).Returns(city);
        _solarInfoReader.Setup(x => x.GetSolarData(It.IsAny<Double>(), It.IsAny<Double>(), It.IsAny<DateOnly>()))
            .Returns("string");
        _solarParser.Setup(x => x.Process(It.IsAny<String>())).Returns(solar);
        var result = _controller.GetSolarInfos("budapest", new DateOnly(2021, 12, 10));
        Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
    }
    
    
}