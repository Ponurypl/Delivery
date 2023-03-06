using System.Collections;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;
public class RecipientCreateWrongData : IEnumerable<object[]>
{
    //companyName, country, flatNumber, lastName, name, phoneNumber, postCode, street, streetNumber, town
    public IEnumerator<object[]> GetEnumerator()
    {
        //yield return new object[] {
        //    "companyname", "country", "flatnumber", "lastname", "name", "phonenumber", "postcode", "street", "streetnumber", "town"
        //};
        //missing town
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "phoneNumber", "postCode", "street", "streetNumber", ""
        };
        //missing town
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "phoneNumber", "postCode", "street", "streetNumber", "  "
        };
        //missing town
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "phoneNumber", "postCode", "street", "streetNumber", null!
        };
        //missing postCode
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "phoneNumber", "", "street", "streetNumber", "town"
        };
        //missing postCode
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "phoneNumber", "    ", "street", "streetNumber", "town"
        };
        //missing postCode
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "phoneNumber", null!, "street", "streetNumber", "town"
        };
        //missing streetNumber
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "phoneNumber", "postCode", "street", "", "town"
        };
        //missing streetNumber
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "phoneNumber", "postCode", "street", "  ", "town"
        };
        //missing streetNumber
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "phoneNumber", "postCode", "street", null!, "town"
        };
        //missing country
        yield return new object[] {
            "companyName", "", "flatNumber", "lastname", "name", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing country
        yield return new object[] {
            "companyName", " ", "flatNumber", "lastname", "name", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing country
        yield return new object[] {
            "companyName", null!, "flatNumber", "lastname", "name", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing phoneNumber
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", "", "postCode", "street", "streetNumber", "town"
        };
        //missing phoneNumber
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", " ", "postCode", "street", "streetNumber", "town"
        };
        //missing phoneNumber
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "name", null!, "postCode", "street", "streetNumber", "town"
        };
        //missing noCredentials
        yield return new object[] {
            "", "country", "flatNumber", "", "", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing noCredentials
        yield return new object[] {
            " ", "country", "flatNumber", " ", " ", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing noCredentials
        yield return new object[] {
             null!, "country", "flatNumber",  null!,  null!, "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing only part of name given
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing only part of name given
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", "  ", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing only part of name given
        yield return new object[] {
            "companyName", "country", "flatNumber", "lastname", null!, "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing only part of name given
        yield return new object[] {
            "companyName", "country", "flatNumber", "", "name", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing only part of name given
        yield return new object[] {
            "companyName", "country", "flatNumber", " ", "name", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing only part of name given
        yield return new object[] {
            "companyName", "country", "flatNumber", null!, "name", "phoneNumber", "postCode", "street", "streetNumber", "town"
        };
        //missing only part of name given
        yield return new object[] {
            "", "", "flatNumber", "", "", "", "", "street", "", ""
        };
        //mix        
        yield return new object[] {
            " ", " ", "flatNumber", " ", " ", " ", " ", "street", " ", " "
        };
        //mix        
        yield return new object[] {
            null!, null!, "flatNumber", null!, null!, null!, null!, "street", null!, null!
        };
        yield return new object[] {
            "", "", "flatNumber", "lastname", "", "", "", "street", "", ""
        };
        //mix        
        yield return new object[] {
            " ", " ", "flatNumber", "lastname", " ", " ", " ", "street", " ", " "
        };
        //mix        
        yield return new object[] {
            null!, null!, "flatNumber", "lastname", null!, null!, null!, "street", null!, null!
        };
        yield return new object[] {
            "", "", "flatNumber", "", "name", "", "", "street", "", ""
        };
        //mix        
        yield return new object[] {
            " ", " ", "flatNumber", " ", "name", " ", " ", "street", " ", " "
        };
        //mix        
        yield return new object[] {
            null!, null!, "flatNumber", null!, "name", null!, null!, "street", null!, null!
        };


    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
