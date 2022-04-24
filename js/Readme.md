# National Health Information System Prescription Dosage Instructions Parser

This package can be used by bulgarian medical and pharmaceutical software developers to parse NHIS e-Prescription dosage instructions.
It exports methods to parse instruction and update according nomenclatures.

Sample usage:

```js
const parser = require('nhis-dosage-parser');
const dose = {
    "sequence":1,
    "doseQuantityValue":1,
    "doseQuantityCode":"78",
    "frequency":2,
    "period":1,
    "periodUnit":"d",
    "boundsDuration":28,
    "boundsDurationUnit":"d",
    "when":[1,12,8,13],
    "offset":15,
    "text":null,
    "route":61
};

console.log(parser.dosageParser(dose));

// 1 таблетка перорално 2 пъти на ден 15 минути сутрин след сън вечер преди сън за 28 дни
```

Please, be aware that needed nomenclatures are build in and are relatively static. They need to be updated only on NHIS specification change. It is strongly advisable not to include nomenclature update in normal program flow as it is relatively slow and is asynchronous by design (it can not be awaited).

Sample nomenclature update:

```js
const parser = require('nhis-dosage-parser');

parser.updateNomenclatures();

```

*Improvements are warmly welcomed!*
