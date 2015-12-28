# Hands-on-lab WeatherStation

This project uses the I2C bus and general purpose input/output (GPIO) ports available on the Raspberry Pi 2, to create an internet connected weather station using the SparkFun weather shield.

The instructions provided will give a developer first-hand experience setting up the required hardware along with writing and debugging the newly available Windows 10, UWP Windows.Devices API's. This lab will also demonstrate how to aggregate your data in cloud using the Azure Event Hub, via the easy-to-use ConnectTheDots API.

# Hardware
![cover-image](http://i.imgur.com/iBiSCS4.png)
*GPIO schematic (pin 1 is marked with a square solder pad)*

![cover-image](http://i.imgur.com/jjYyZaR.jpg)
*Wiring close-up of the interior rail (brown, yellow, orange, green)*

![cover-image](http://i.imgur.com/jjYyZaR.jpg)
*Wiring close-up of the exterior rall (black, red)*

![cover-image](http://i.imgur.com/Iu07gGY.jpg)
*Wiring clouse-up of the left rail (blue, green, yellow, orange)*

![cover-image](http://i.imgur.com/sYZpmT4.jpg)
Wiring clouse-up of the right rail (brown, black, red)*

## Pinout Diagram (Raspberry Pi 2 --> Sparkfun weather shield):


  - GND-------(black)------GND
  - 5V----------(red)---------VIN
  - 3V3-------(brown)------5V (shield hack; not a typo)
  - GPIO2-----(yellow)----SDA
  - GPIO3----(orange)----SCL
  - GPIO5-----(green)-----D8
  - GPIO6-----(blue)-------D7


# Software
The weather station is actually two applications. The first is a long running (indefinitely, actually) background task that reads the sensors and acts as a weather station server. The second, a UI that makes a request to port 50001 of the server and displays the data. The UI application is universal and can be deployed on any Windows device from the Raspberry Pi 2 all the way to a desktop PC - and anywhere in between.

You need to find the following line in the `Mainpage.xaml.cs` file from the `weather-station` project, and replace the computer name, "minwinpc", in the URL with the name of your IoT device.

## Install the weather station application:

Clone the linked repository (using the --recursive flag)

select the "master" branch if you want the completed code

Open "WeatherStation\WeatherStation.sln" in Visual Studio 2015

Navigate to "WeatherShield.cs" in the "Solution Explorer" pane

If you chose the lab branch, Navigate to “View >> Other Windows >> Task List”, to view the remaining work (depicted above).

You will notice there is quite a bit of detail in the comment to help you complete the task. However, if you still need that extra nudge, there will be a “HINT” provided to remind you to look to nearby code for help (illustrated above).

Once the //TODO:'s have been completed, click the “Debug” menu item, and select “WeatherStation Properties…”

Under the “Debug” tab, in the “Start options” section

Select “Remote Device” as “Target device:”
Enter the IP address of your Windows IoT Core device in the “Remote machine:” field
Deploy to the Windows IoT Core device

## Integrating

Open "WeatherStation\WeatherStation.sln" in Visual Studio 2015

Navigate to "WeatherStationTask.cs" in the "Solution Explorer" pane

Use the "Task List" to jump to each “//TODO:” and write the necessary code

The AppSettings, ConnectTheDotsSensor, and ConnectTheDotsHelper files are all part of the code created to help you use the ConnectTheDots interface to the Azure Event Hub.

AppSettings: Saves the settings for connecting to the Event Hub

This information can be found under your ServiceBus in Azure.

Go to your "*-ns" servicebus instance -> Event Hubs -> ehdevices -> Connection Information -> Look for the SAS "D1"

Copy the connection string which should look like this (It contains information for your AppSettings)

"Endpoint=sb://iotbuildlab-ns.servicebus.windows.net/;SharedAccessKeyName=D1;SharedAccessKey=iQFNbyWTYRBwypMtPmpfJVz+NBgR32YHrQC0ZSvId20="

service bus namespace (Ex:  "iotbuildlab-ns")
event hub name (Ex: "ehdevices" - always use this)
key name (Ex: "D1")
key (Ex: " iQFNbyWTYRBwypMtPmpfJVz+NBgR32YHrQC0ZSvId20=")
display name (Ex: "WeatherStation1" - This gives a name to the device data)
organization (Ex: "IoT Build Lab" - Change to customize)
location (Ex: "USA" - Change to customize)
ConnectTheDotsSensor: Contains the information for a sensor

guid
display name
organization
location
measure name
unit of measure
time created
value
ConnectTheDotsHelper: Helper functions to initialize the Event Hub

establishes the connection
creates the authentication tokens

# COMPONENTS AND SUPPLIES

- Raspberry Pi 2 Model B
- Power supply (5V 1.2A)
- 16GB SD Card (preferably Samsung EVO)
- SparkFun Weather Shield
- Ribbon cable (7 wires, male -> female)
- Cat-5e Ethernet Cable

# Sample Code

## This can be accessed from the repository.

# Youtube Video
[![Everything Is AWESOME](http://i.imgur.com/hnpavfk.png)](https://www.youtube.com/embed/Hkm4THS3Rf8 "Everything Is AWESOME")

