using ConsoleTest;
using System.Numerics;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

/*
var list = new List<TestObj>() {  };


try
{
    var last = list.LastOrDefault(x=> x.Age == 5);
    var aa = last.Age;
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}


var obj = new JournalEntryCreateRequestBulkMessage();
obj.MessageHeader = new BusinessDocumentMessageHeader()
{
    ID = new BusinessDocumentMessageID()
    {
        Value = "MSG_2024-03-28"
    },
    CreationDateTime = DateTime.Now
};
obj.JournalEntryCreateRequest = new JournalEntryCreateRequestMessage[] {
    new JournalEntryCreateRequestMessage()
    {
        MessageHeader = new BusinessDocumentMessageHeader()
        {
            ID = new BusinessDocumentMessageID()
            {
                Value = "SUB_MSG_2024-03-28"
            },
            CreationDateTime = DateTime.Now
        },
        JournalEntry = new JournalEntryCreateRequestJournalEntry()
        {
            DocumentDate = DateTime.Now,
            PostingDate = DateTime.Now,
            Item = new JournalEntryCreateRequestJournalEntryItem[]
            {
                new JournalEntryCreateRequestJournalEntryItem()
                {
                    ReferenceDocumentItem = "1",
                    GLAccount = new ChartOfAccountsItemCode()
                    {
                        Value = "21000002"
                    },
                    AmountInTransactionCurrency = new Amount()
                    {
                        currencyCode = "BRL",
                        Value = 100
                    },
                    DebitCreditCode = "H",
                    HouseBank = "BRBK1",
                    HouseBankAccount = "BRAC1"
                },
                new JournalEntryCreateRequestJournalEntryItem()
                {
                    ReferenceDocumentItem = "2",
                    GLAccount = new ChartOfAccountsItemCode()
                    {
                        Value = "21000001"
                    },
                    AmountInTransactionCurrency = new Amount()
                    {
                        currencyCode = "BRL",
                        Value = -100
                    },
                    DebitCreditCode = "S",
                    HouseBank = "BRBK2",
                    HouseBankAccount = "BRAC2"
                }
            },
        }
    }
};

BasicHttpsBinding binding = new BasicHttpsBinding()
{
    MaxBufferSize = 2147483647,
    MaxReceivedMessageSize = 2147483647,
    Name = "WMSOperSoap"
};
EndpointAddress endpoint = new EndpointAddress("https://my404561-api.s4hana.cloud.sap");

var client = new JournalEntryCreateRequestConfirmation_InClient(binding, endpoint);
client.ClientCredentials.UserName.UserName = "S4HBTP_USER";
client.ClientCredentials.UserName.Password = "VgkuzGLZHxFCWmT3fpibHEadeXNishWUGS/EnbPj";


client.Close();

var setting = new XmlWriterSettings()
{
    OmitXmlDeclaration = true,
};
StringWriter stringWriter = new StringWriter();
XmlWriter xmlWriter = XmlWriter.Create(stringWriter, setting);
XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
namespaces.Add("sfin", "http://sap.com/xi/SAPSCORE/SFIN");
namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");

/*
var e = new Envelope(obj);


var mySerializer = new XmlSerializer(typeof(Envelope));
mySerializer.Serialize(xmlWriter, e, namespaces);
string str = stringWriter.ToString();

xmlWriter.Close();
stringWriter.Close();

var envelopeSTR = $@"
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:sfin=""http://sap.com/xi/SAPSCORE/SFIN"">
    <soapenv:Header/>
    <soapenv:Body>
        {str}
    </soapenv:Body>
</soapenv:Envelope>";

Console.WriteLine(envelopeSTR);

using (StringReader reader = new StringReader(xmlString))
{
    var myObject = (S4HanaSOAP.JournalEntryCreateRequestBulkMessage)mySerializer.Deserialize(reader);
}

var str = "\r\n\r\n\r\n<soap-env:Envelope xmlns:soap-env=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap-env:Header/><soap-env:Body><n0:JournalEntryBulkCreateConfirmation xmlns:n0=\"http://sap.com/xi/SAPSCORE/SFIN\" xmlns:prx=\"urn:sap.com:proxy:XGJ:/1SAI/TAS3D871C7855B59197A671:795\"><MessageHeader><UUID>2d656d48-6d1b-1eee-bc82-9fbbd1ef35ff</UUID><ReferenceID>MSG_2024-03-28</ReferenceID><CreationDateTime>2024-04-01T10:45:44.484251Z</CreationDateTime><SenderBusinessSystemID>XGJ</SenderBusinessSystemID></MessageHeader><ConfirmationInterfaceOrignName/><JournalEntryCreateConfirmation><MessageHeader><UUID>2d656d48-6d1b-1eee-bc82-9fbbd1ef55ff</UUID><ReferenceID>SUB_MSG_2024-03-28</ReferenceID><CreationDateTime>2024-04-01T10:45:44.484353Z</CreationDateTime></MessageHeader><JournalEntryCreateConfirmation><AccountingDocument>0100000008</AccountingDocument><CompanyCode>1410</CompanyCode><FiscalYear>2024</FiscalYear></JournalEntryCreateConfirmation><Log><MaximumLogItemSeverityCode>1</MaximumLogItemSeverityCode><Item><TypeID>605(RW)</TypeID><SeverityCode>1</SeverityCode><Note>Document posted successfully: BKPFF 010000000814102024 0M4SMVI</Note><WebURI>http://vhshpxgjci.shp02.cos.s4h.sap.corp:50000/sap/xi/docu_apperror?ID=NA&amp;OBJECT=RW605&amp;LANGUAGE=E&amp;MSGV1=BKPFF&amp;MSGV2=010000000814102024&amp;MSGV3=0M4SMVI</WebURI></Item></Log></JournalEntryCreateConfirmation><Log/></n0:JournalEntryBulkCreateConfirmation></soap-env:Body></soap-env:Envelope>";
*/


// Criar uma nova instância do RSACryptoServiceProvider
using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048)) //2048 // Você pode escolher o tamanho da chave aqui
{
    try
    {
        // password lenght 20
        // Exportar a chave pública
        /*
        <RSAKeyValue
        ><Modulus>t4YYaijWtnaM9aKRe2cxQQ/oqlw7nav2uOsrfnQqLlw+WpKJYRIpaZYLsg6TPs8N3LlQMGfQpl8ntuGwey3d5Q==</Modulus>
        <Exponent>AQAB</Exponent>
        <P>wfimr6liAWwxnp95141dsAQ69aeGF0Ac6hR4yR/WG0c=</P>
        <Q>8jYqwOLHRpF2xixvcPg0UcHHu9CruKF2TIf55PM3+3M=</Q>
        <DP>k1H1z263R9tTqGT5FjSBFoFwAkl0902zaDmaLy8l31s=</DP>
        <DQ>Ki4eseqMU5C/g3F3ks/WpKo/c0i6rvOHW3qOnT1x8Vs=</DQ>
        <InverseQ>sgkHsfDZ0Ddcj81YNFLLIDRLziEfQlEHNqzj5bdLVzw=</InverseQ>
        <D>ouqVTdhHEt8Wrw/L2iJ3JOy8fE84VPXNJccOa0kwEMorEkBujMLXKfpkSLYeKWa6Doe7SogWEp2AwRh+qHRdWQ==</D>
        </RSAKeyValue>
        */
        rsa.ImportParameters(new RSAParameters()
        {
            Modulus = Convert.FromBase64String("tZrHToLCRkTub3TVbNWAPe1CLqxrZlpr+Ui+9uyfqVihaWM/7huyiQzQ3AQG9Og9cyCFbaeQtDpB8dbKTfDUO/T/ksw7bFH+MFdY5MtE2rw/JLSsyKpWabNg7rXE/XjKzjy4f9kPW2OXlmMVYnFvRWxmaqQ3WA0poL6Vj7CvYktKCuOVLAN7D2HOU8bawQvmnC+IVFI0SsTSWUszH9bYLgSNQufDo5WsBZgwQezFpHd0Qu+ao8IFVpdnkWu07foMSGuBHJqKMRx/gmXnK/2NMyvG/H3n8Y3Htze0AxpUdFiidUO3Z2EaDVsk1ZeHAm5t1uZtxV88ZH/I4SxShms9tQ=="),
            Exponent = Convert.FromBase64String("AQAB"),
            P = Convert.FromBase64String("7dDSAi1IUYNJnZDYs1zmwbWl9+s00oGf9/DFDnV6Y1Gi4V9PvHaK+yhcwBC8T+QVtSaxdU99GjGq8TS4/0RU2XXhCYpZ+rVWeijOY75YN04npQDNZn45yUE7/UAtAwUlg3pB+p97O30Yq38VD6boP4r/1xz6vXIrYYL4/i7fkUM="),
            Q = Convert.FromBase64String("w32kBwiQsV/JARu8xwbChRVwZdmYRkzJ1fZqwjrgRgJEqi1iAMq3kz1KpdX/cogQSaXAeU3cjccmV7FAYJYthriO85oWvsPXD7x8a+BDzMANkDEWA5TL+BvSNGvZuQUWS+DfJ/7rX61JeZjW+m7gR6Aw0ECQXVSHbi9oHzyVaac="),
            DP = Convert.FromBase64String("aWKqTdCnRYf6bVqszP8UOy819yFB7S8IJqWJjZi9vZmFpn3IJeoaOKZwQ6Sm7nhSrk4RQb0R4TO1XuJQQL6VIzC7orCuow0M32GJ5GWLTibSJqquWwcAsJC59sYjrDzxnMvmxRs7TudmsUjFmoQKU25TZY4wBxe65aFWmb1H0WU="),
            DQ = Convert.FromBase64String("j7m0ubMTZkUM6/KvpQgURXIQs+D5sl3MWrGTpf9RWkdaol9BWWGw++CpHCmsaFEe4HarfsO+7sHHL9vSf/CJwi24c+MK6+/iEuC5/TnhvwxILIXaMIHaQCx8LCxK41uZG+pVRvuFo08s4Oh8zZxs6lwc509AlE2MUHcsLyMEmqs="),
            InverseQ = Convert.FromBase64String("ewvSwOOJ+fVKpRm8uq/aM+0wJ2RMWgBSARlxjKjMkQCZc0m9dRi2A7wvvYNrPw5PqxGY71rybupQ0X5QvRFub/czk9/ro7RjKkDpNR8/g4jrVj+/g08Njut+MxUrpBfQ0BlPxkItFOIgJyz74Wjn6YCyzbKhutUeZGd9/KyUOyE="),
            D = Convert.FromBase64String("KoCA6MT9hsiXLQqg8Im/K78dYCMGN7wtzPfTiaKzc0TE8g9CZQ2iaSTQtM1Ue3jm+MzBrXosCaIg0OK+EiOs6CNctRSLS7ycyt+GdHWGNdMczBBk58nF05MhunxcEB53CHF5lKJOT8stLQsfdBysOAk4pvir4zWhandwNWLaiRxEXMUenowt9+DIfE64COMOGDfVmCVE24PJtJLoJMNhi7aCMgzOzbA6HLaUBWehI0sdPJAI9K70OwAlKWbmtlmps6MDrc7LhGCupwgyKOqM2FPEpzAlCTG4mt1gHdT+NMduOI2jWK4XiDNSVyeNYy0ooUM3nhEtlOrl3qe8VAUSeQ==")
        });

        string publicKey = rsa.ToXmlString(false);

        // "012345678901234567890123456789012345678901234567890" 50
        // A chave a ser criptografada 200
        // 344
        string keyToEncrypt = "0"; // "012345678901234567890123456789012345678901234567890012345678901234567890123456789012345678901234567890012345678901234567890123456789012345678901234567890012345678901234567890123456789012345678901234567890";// "MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345MinhaChaveGrande12345";

        string privateKey = rsa.ToXmlString(true);
        byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(keyToEncrypt);

        // Criptografar a chave
        byte[] encryptedData = rsa.Encrypt(bytesToEncrypt, false);

        // Exibir a chave criptografada (em base64)
        string encryptedKey = Convert.ToBase64String(encryptedData);
        Console.WriteLine("Chave Criptografada: " + encryptedKey);

        // Descriptografar a chave criptografada
        byte[] bytesToDecrypt = Convert.FromBase64String(encryptedKey);
        byte[] decryptedData;

        using (RSACryptoServiceProvider rsaDecrypt = new RSACryptoServiceProvider(2048))
        {
            rsaDecrypt.FromXmlString(privateKey);
            decryptedData = rsaDecrypt.Decrypt(bytesToDecrypt, false);
        }

        // Converter os bytes descriptografados de volta para string
        string decryptedKey = Encoding.UTF8.GetString(decryptedData);
        Console.WriteLine("Chave Descriptografada: " + decryptedKey);
        Console.WriteLine("Chave é igual: "+ (keyToEncrypt == decryptedKey).ToString());
    }
    finally
    {
        // Liberar os recursos do RSACryptoServiceProvider
        rsa.PersistKeyInCsp = false;
    }
}

Console.ReadKey();