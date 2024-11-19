using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch;
using SolarWatch.Controllers;
using SolarWatch.Services;
using SolarWatch.Services.JsonParsers;
using SolarWatch.Services.Repositories;

namespace SolarWatchTest;

public class SolarControllerTest
{
    private Mock<ILogger<SolarWatchController>> _mockLogger;
    private Mock<ICityDataProvider> _cityApiReader;
    private Mock<ISolarInfoProvider> _solarInfoReader;
    private Mock<ICityParser> _cityParser;
    private Mock<ISolarParser> _solarParser;
    private SolarWatchController _controller;
    private Mock<ICityRepository> _cityRepository;
    private Mock<ISolarRepository> _solarRepository;

    [SetUp]
    public void Setup()
    {
        _cityApiReader = new Mock<ICityDataProvider>();
        _solarInfoReader = new Mock<ISolarInfoProvider>();
        _cityParser = new Mock<ICityParser>();
        _solarParser = new Mock<ISolarParser>();
        _mockLogger = new Mock<ILogger<SolarWatchController>>();
        _cityRepository = new Mock<ICityRepository>();
        _solarRepository = new Mock<ISolarRepository>();
        _controller = new SolarWatchController(_mockLogger.Object, _cityApiReader.Object, _cityParser.Object, _solarInfoReader.Object, _solarParser.Object, _solarRepository.Object, _cityRepository.Object);
    }

    [Test]
    public async Task GetSolarInfosReturnsNotFoundWhenCityApiFailed()
    {
        _cityApiReader.Setup(x => x.GetCityData(It.IsAny<string>())).Throws(new Exception());
        var result = await _controller.GetSolarInfos("budapest", new DateOnly(2021, 12, 10));
        Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
    }

    [Test]
    public async Task TestWithCorrectCityNameAndDateWithDataInDb()
    {
        City city = new City();
        _cityRepository.Setup(x => x.GetByName(It.IsAny<string>())).Returns(city);
        var result = await _controller.GetSolarInfos("budapest", new DateOnly(2021, 12, 10));
        Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
    }

    [Test]
    public async Task TestWithCorrectCityAndDateWithoutDataInDb()
    {
        City city = new City();
        // returns null because we don't have this data in our DB yet.
        _cityRepository.Setup(x => x.GetByName(It.IsAny<string>())).Returns((City?)null);
        // Get the data from API instead
        _cityApiReader.Setup(x => x.GetCityData(It.IsAny<string>())).ReturnsAsync("city");
        _cityParser.Setup(x => x.Process(It.IsAny<string>())).Returns(city);
        var result = await _controller.GetSolarInfos("budapest", new DateOnly(2021, 12, 10));
        Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
    }
    
    
}