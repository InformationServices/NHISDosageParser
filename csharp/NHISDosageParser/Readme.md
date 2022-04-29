# National Health Information System Prescription Dosage Instructions Parser

This package can be used by bulgarian medical and pharmaceutical software developers to parse NHIS e-Prescription dosage instructions.
It exports methods to parse instruction and update according nomenclatures. In order to use it with .Net Core Nuget Package must be installed.

For .Net Core 3.1 use:

...

For .Net 6.0 use:

...

For .Net Framework, please check the implementation in branch csharp-version-47

Sample usage:

```c#
ServiceProvider services = new ServiceCollection()
    .AddNhisDosageParser()
    .BuildServiceProvider();

var parser = services.GetService<INhisDosageParser>();
var dosageInstruction = new NhisDosageInstruction()
{
    Sequence = 1,
    DoseQuantityValue = 1,
    DoseQuantityCode = "78",
    Frequency = 2,
    Period = 1,
    PeriodUnit = "d",
    BoundsDuration = 28,
    BoundsDurationUnit = "d",
    When = new string[] { "1", "12", "8", "13" },
    Offset = 15,
    Text = null,
    Route = "61"
};

Console.WriteLine(parser.DosageParser(dosageInstruction));

// 1 таблетка перорално 2 пъти на ден 15 минути сутрин след сън вечер преди сън за 28 дни
```

Please, be aware that needed nomenclatures are build in and are relatively static. They need to be updated only on NHIS specification change. It is strongly advisable not to include nomenclature update in normal program flow as it is relatively slow.

Sample nomenclature update:

```c#
ServiceProvider services = new ServiceCollection()
    .AddNhisDosageParser()
    .BuildServiceProvider();

var parser = services.GetService<INhisDosageParser>();

await parser.UpdateNomenclatures();

```

*Improvements are warmly welcomed!*
