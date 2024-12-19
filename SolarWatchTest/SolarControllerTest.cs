using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        Solar solar = new Solar();
        _cityRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync(city);
        _solarRepository.Setup(x => x.Get(It.IsAny<DateOnly>(), It.IsAny<int>())).ReturnsAsync(solar);
        var result = await _controller.GetSolarInfos("budapest", new DateOnly(2021, 12, 10));
        Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
        Assert.That(((OkObjectResult)result.Result).Value, Is.EqualTo(solar));
    }

    [Test]
    public async Task TestWithCorrectCityAndDateWithoutDataInDb()
    {
        City city = new City();
        Solar solar = new Solar();
        // returns null because we don't have this data in our DB yet.
        _cityRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync((City?)null);
        // Get the data from API instead
        _cityApiReader.Setup(x => x.GetCityData(It.IsAny<string>())).ReturnsAsync("city");
        _cityParser.Setup(x => x.Process(It.IsAny<string>())).Returns(city);
        _solarInfoReader.Setup(x => x.GetSolarData(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateOnly>()))
            .ReturnsAsync("solar");
        _solarParser.Setup(x => x.Process(It.IsAny<string>(), It.IsAny<City>(), It.IsAny<DateOnly>())).Returns(solar);
        var result = await _controller.GetSolarInfos("budapest", new DateOnly(2021, 12, 10));
        Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
        Assert.That(((OkObjectResult)result.Result).Value, Is.EqualTo(solar));
    }
    
    
}