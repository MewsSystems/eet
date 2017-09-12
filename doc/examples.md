# Advanced examples

### Verification mode
```csharp
var response = await client.SendRevenueAsync(record, EetMode.Verification);
```

### Using playground
```csharp
var client = new EetClient(certificate, EetEnvironment.Playground);
```

### Timeouts

The default HTTP request timeout is set to 2 seconds as per the Czech law. If you want to change it, you need to pass a parameter to the EetClient constructor:

```csharp
var client = new EetClient(certificate, EetEnvironment.Playground, httpTimeout: TimeSpan.FromSeconds(20));
```

### Logging
- Catchall logger:
```csharp
Action<string, object> logHandler = (message, detailsObject) => { ... };
var client = new EetClient(
    certificate,
    logger: logHandler
);
```

- Selective logger:
```csharp
Action<string, object> logHandler = (message, detailsObject) => { ... };
var client = new EetClient(
    certificate,
    logger: new EetLogger(onError: logHandler, onInfo: logHandler, onDebug: null)
);
```

### Events
- HTTP request duration
```csharp
var client = new EetClient(certificate);
client.HttpRequestFinished += (sender, args) =>
{
    var duration = args.Duration;
};
```

- Catching XML message sent to EET
```csharp
var client = new EetClient(certificate);
client.XmlMessageSerialized += (sender, args) =>
{
    var xmlString = args.XmlElement.OuterXml;
};
```
### Storing Certificate in Certificate store

- Install the provided certificate in Windows certificate store 
- Create an instance of CertificatesStoreParams class and assign values to all the properties accordingly
            StoreLocation = StoreLocation.CurrentUser (or StoreLocation.LocalMachine),
            StoreName = StoreName.My (or StoreName.My or StoreName.TrustedPeople etc),
            FindType = X509FindType.FindBySubjectName (or X509FindType.FindBySerialNumber etc),
            FindValue = "CZ00000019" (the value of the field, which we are stating in "FindType" property)
- FindValue: Open certificate store and double click on the certificate. Navigate to Details tab on the pop-up. 
			 You can see the fields and there respective values.
			 
- To see the complete usage please open the Basics.cs inside the Mews.Eet.Tests project and go through the below test methods:
			SendRevenueUsingCertificateStore
			SendRevenueRecordUsingCertificateStore
