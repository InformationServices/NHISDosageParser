
const fs = require("fs");
const https = require("https");
const { v4: uuidv4 } = require('uuid');
const parseStringPromise = require('xml2js').parseStringPromise;

/**
 * Loads CL013, CL020, CL034 and CL035
 * from files into JS Objects
 * @returns array of objects
 */
const loadNomenclatures = () => {
    const result = [];

    let cl013 = fs.readFileSync(`${__dirname}/cl013.json`);
    let cl020 = fs.readFileSync(`${__dirname}/cl020.json`);
    let cl034 = fs.readFileSync(`${__dirname}/cl034.json`);
    let cl035 = fs.readFileSync(`${__dirname}/cl035.json`);

    result.push(JSON.parse(cl013));
    result.push(JSON.parse(cl020));
    result.push(JSON.parse(cl034));
    result.push(JSON.parse(cl035));
        
    return result;
};

/**
 * Parses and saves CL013 to file 
 * @param {object} nom - received nomenclature (parsed C002)
 */
const saveCL013 = (nom) => {
    const cl013 = {
        'dateUpdated': (new Date()).toISOString(),
        'nom': {}
    };

    const entries = nom["nhis:message"]["nhis:contents"][0]["nhis:nomenclature"][0]["nhis:entry"];
    entries.forEach(element => {
        cl013["nom"][element["nhis:key"][0]["$"]["value"]] = element["nhis:description"][0]["$"]["value"].trim();
    });

    fs.writeFileSync(`${__dirname}/cl013.json`, JSON.stringify(cl013, null, 4), 'utf8');
};

/**
 * Parses and saves CL020 to file 
 * @param {object} nom - received nomenclature (parsed C002)
 */
const saveCL020 = (nom) => {
    const cl020 = {
        'dateUpdated': (new Date()).toISOString(),
        'nom': {},
        'nomPlural': {}
    };

    const entries = nom["nhis:message"]["nhis:contents"][0]["nhis:nomenclature"][0]["nhis:entry"];
    entries.forEach(element => {
        cl020["nom"][element["nhis:key"][0]["$"]["value"]] = element["nhis:description"][0]["$"]["value"].trim();
        element["nhis:meta"].forEach(item => {
            if (item["nhis:name"][0]["$"]["value"] === "plural") {
                cl020["nomPlural"][element["nhis:key"][0]["$"]["value"]] = item["nhis:value"][0]["$"]["value"].trim();
            }
        });
    });

    fs.writeFileSync(`${__dirname}/cl020.json`, JSON.stringify(cl020, null, 4), 'utf8');
};

/**
 * Parses and saves CL034 to file 
 * @param {object} nom - received nomenclature (parsed C002)
 */
const saveCL034 = (nom) => {
    const cl034 = {
        'dateUpdated': (new Date()).toISOString(),
        'nom': {}
    };

    const entries = nom["nhis:message"]["nhis:contents"][0]["nhis:nomenclature"][0]["nhis:entry"];
    entries.forEach(element => {
        cl034["nom"][element["nhis:key"][0]["$"]["value"]] = element["nhis:description"][0]["$"]["value"].trim();
    });

    fs.writeFileSync(`${__dirname}/cl034.json`, JSON.stringify(cl034, null, 4), 'utf8');
};

/**
 * Parses and saves CL035 to file 
 * @param {object} nom - received nomenclature (parsed C002)
 */
const saveCL035 = (nom) => {
    const cl035 = {
        'dateUpdated': (new Date()).toISOString(),
        'nom': {},
        'nomPlural': {}
    };

    const entries = nom["nhis:message"]["nhis:contents"][0]["nhis:nomenclature"][0]["nhis:entry"];
    entries.forEach(element => {
        cl035["nom"][element["nhis:key"][0]["$"]["value"]] = element["nhis:description"][0]["$"]["value"].trim();
        element["nhis:meta"].forEach(item => {
            if (item["nhis:name"][0]["$"]["value"] === "plural") {
                cl035["nomPlural"][element["nhis:key"][0]["$"]["value"]] = item["nhis:value"][0]["$"]["value"].trim();
            }
        });
    });

    fs.writeFileSync(`${__dirname}/cl035.json`, JSON.stringify(cl035, null, 4), 'utf8');
};

/**
 * Routes the received nomenclature
 * to proper save method
 * @param {object} nom - parsed C002
 * @param {string} name - name of the nomenclature 
 */
const saveNomenclatures = (nom, name) => {
    switch (name.toUpperCase()) {
        case 'CL013':
            saveCL013(nom);
            break;
        case 'CL020':
            saveCL020(nom);
            break;
        case 'CL034':
            saveCL034(nom);
            break;
        case 'CL035':
            saveCL035(nom);
            break;
    
        default:
            break;
    }
};

/**
 * Sends request to NHIS and processes received response
 * @param {string} name - nomenclature name
 */
const update = (name) => {
    let date = new Date();
    let data = fs.readFileSync(`${__dirname}/request.template`, 'utf8');
    let messageId = uuidv4();
    let xmlRes = '';

    data = data
        .replace('{{messageId}}', messageId)
        .replace('{{createdOn}}', date.toISOString())
        .replace('{{nomenclatureId}}', name.toUpperCase());

    const options = {
        hostname: 'api.his.bg',
        port: 443,
        path: '/v1/nomenclatures/all/get',
        method: 'POST',
        headers: {
            'Content-Type': 'application/xml',
            'Content-Length': data.length
        }
    };
          
    const req = https.request(options, res => {    
        res.on('data', d => {
          xmlRes += d;
        });

        res.on('end', () => {
            parseStringPromise(xmlRes)
                .then(parsed => {
                    saveNomenclatures(parsed, name);
                })
                .catch(error => {
                    throw error;
                });
        });
    });
    
    req.on('error', error => {
        throw error;
    });
    
    req.write(data);
    req.end();
}

/**
 * Updates all needed nomenclatures
 * This method is used by dosage parser
 * to update cashed nomenclatures
 */
const updateNomenclatures = () => {
    update('cl013');
    update('cl020');
    update('cl034');
    update('cl035');
};

exports.loadNomenclatures = loadNomenclatures;
exports.updateNomenclatures = updateNomenclatures;