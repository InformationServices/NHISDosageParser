const nomenclatures = require("./nomenclatures");
let cl013, cl020, cl034, cl035, forms;

/**
 * Processing A and B:
 *  doseQuantityValue (A) -> represent as a number
 *  doseQuantityCode (B)
 *   -> represent as UCUM unit without brackets
 *   -> represent as CL035 description
 */
const convertDosageFormToText = ( form, isSingle ) => {
    let result = "";
    
    if (form) {
        result = (isSingle === true) ? forms["default"].single : forms["default"].plural;
    }
    
    if (forms[form]) {
        result = (isSingle === true) ? forms[form].single : forms[form].plural;
    }

    return result;
}

/**
 * Processing D and E:
 *  period (D) -> represent as: if D=1 then skip else if D>1 then as a number
 *  periodUnit (E) -> represent as textual description:
 *   -> s -> if D=1 then "секунда" else if D>1 then "секунди"
 *   -> min -> if D=1 then "минута" else if D>1 then "минути"
 *   -> h -> if D=1 then "час" else if D>1 then "часа"
 *   -> d -> if D=1 then "ден" else if D>1 then "дни"
 *   -> wk -> if D=1 then "седмица" else if D>1 then "седмици"
 *   -> mo -> if D=1 then "месец" else if D>1 then "месеца"
 *   -> a -> if D=1 then "година" else if D>1 then "години"
 */
const convertTimeToText = ( time, isSingle ) => {
    return (isSingle === true) ? cl020["nom"][time] : cl020["nomPlural"][time];
}

/**
 * Processing H and I (optional):
 *  when (H) -> represent as CL034 description
 *  offset (I) -> represent as a number
 */
 const convertOffsetToText = ( offset ) => {
    return ( offset ) ? offset + ( ( offset > 1 ) ? " минути" : " минута" ) : "";
};

const concatWhens = ( whens, nomenclature ) => {
    let whenStr = "";

    if(whens) {
        whens.forEach((element) => whenStr = whenStr + nomenclature["nom"][ element ] + " ");
    }

    return whenStr;
}

const dosageParser = ( dosage ) => {
    [cl013,cl020, cl034, cl035] = nomenclatures.loadNomenclatures();
    forms = nomenclatures.loadMedicineDosageForm();

    /**
     * Full formula:
     *  A + B + (if K then K else skip) + C + "на" + (if D>1 then "всеки" else skip) + D + Е
     *  + (if H then ( if I then I + "минути" else skip) + H else "")
     *  + (if F then "за" + F + G else skip)
     *  + (if J then "\n" + J else skip)
     */
    let dosageInstructions = "";

    dosageInstructions += dosage.doseQuantityValue + " " + ( ( cl035["nom"][ dosage.doseQuantityCode ] ) ? convertDosageFormToText( dosage.doseQuantityCode, dosage.doseQuantityValue <= 1 ) : dosage.doseQuantityCode ) + " ";

    /**
     * Processing K (optional):
     *  route (K) -> represent as CL013 description
     */
    dosageInstructions += ( dosage.route && cl013["nom"][ dosage.route ] ) ? cl013["nom"][ dosage.route ] + " " : "";

    /**
     * Processing C:
     *  frequency (C) -> represent as: if C=1 then "веднъж" else as a number + "пъти"
     */
    dosageInstructions += ( dosage.frequency === 1 ) ? "веднъж " : dosage.frequency + " пъти ";
    dosageInstructions += "на " + ( ( dosage.period > 1 ) ? "всеки " + dosage.period + " " : "" );
    dosageInstructions += convertTimeToText( dosage.periodUnit, dosage.period === 1 ) + " ";    
    dosageInstructions += ( ( dosage.when ) ? convertOffsetToText( dosage.offset ) + " " + concatWhens(dosage.when, cl034) : "" );

    /**
     * Processing F and G (optional):
     *  boundsDuration (F) -> represent as a number
     *  boundsDurationUnit (G) -> represent as textual description:
     *   -> s -> if F=1 then "секунда" else if F>1 then "секунди"
     *   -> min -> if F=1 then "минута" else if F>1 then "минути"
     *   -> h -> if F=1 then "час" else if F>1 then "часа"
     *   -> d -> if F=1 then "ден" else if F>1 then "дни"
     *   -> wk -> if F=1 then "седмица" else if F>1 then "седмици"
     *   -> mo -> if F=1 then "месец" else if F>1 then "месеца"
     *   -> a -> if F=1 then "година" else if F>1 then "години"
     */
    dosageInstructions += ( ( dosage.boundsDuration ) ? "за " + dosage.boundsDuration + " " + convertTimeToText( dosage.boundsDurationUnit, dosage.boundsDuration === 1 ) + " " : "" );
    dosageInstructions = (dosageInstructions.length > 0) ? dosageInstructions.charAt(0).toUpperCase() + dosageInstructions.substring(1).toLowerCase() : dosageInstructions;
    /**
     * Processing J (optional):
     *  text (J) -> represent directly below the rest of the dosage instructions
     */
    dosageInstructions += ( ( dosage.text ) ? "\n\r " + dosage.text : "" );

    return dosageInstructions;
};

exports.dosageParser = dosageParser;
exports.updateNomenclatures = nomenclatures.updateNomenclatures;