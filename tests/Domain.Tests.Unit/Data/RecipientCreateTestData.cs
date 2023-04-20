using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;
public static class RecipientCreateTestData 
{
    //companyName, country, flatNumber, lastName, name, phoneNumber, postCode, street, streetNumber, town
    public static IEnumerable<object[]> Create_InvalidData()
    {
        var fixture = new DomainFixture();
        //    "companyname",
        // "country", "flatnumber", "lastname", "name", "phonenumber", "postcode", "street", "streetnumber", "town"

        //missing town
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         ""
                       };
        //missing town
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         " "
                     };
        //missing town
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         null!
                     };
        //missing postCode
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         "",
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing postCode
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         " ",
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing postCode
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         null!,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing streetNumber
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         "",
                         fixture.Recipients.Town
                     };
        //missing streetNumber
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         " ",
                         fixture.Recipients.Town
                     };
        //missing streetNumber
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         null!,
                         fixture.Recipients.Town
                     };
        //missing country
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         "",
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing country
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         " ",
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing country
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         null!,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing phoneNumber
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         "",
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing phoneNumber
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         " ",
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing phoneNumber
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         fixture.Recipients.Name,
                         null!,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing noCredentials
        yield return new object[] 
                     {
                         "",
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         "",
                         "",
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing noCredentials
        yield return new object[] 
                     {
                         " ",
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         " ",
                         " ",
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing noCredentials
        yield return new object[] 
                     {
                         null!,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         null!,
                         null!,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing only part of name given
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         "",
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing only part of name given
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         " ",
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing only part of name given
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         null!,
                         fixture.Recipients.Name,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing only part of name given
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         "",
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing only part of name given
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         " ",
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //missing only part of name given
        yield return new object[] 
                     {
                         fixture.Recipients.CompanyName,
                         fixture.Recipients.Country,
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName,
                         null!,
                         fixture.Recipients.PhoneNumber,
                         fixture.Recipients.PostCode,
                         fixture.Recipients.Street,
                         fixture.Recipients.StreetNumber,
                         fixture.Recipients.Town
                     };
        //mix
        yield return new object[] 
                     {
                         "", 
                         "", 
                         fixture.Recipients.FlatNumber, 
                         "", 
                         "", 
                         "", 
                         "",
                         fixture.Recipients.Street,
                         "", 
                         ""
                     };
        //mix        
        yield return new object[] 
                     {
                         " ", 
                         " ",
                         fixture.Recipients.FlatNumber,
                         " ", 
                         " ", 
                         " ", 
                         " ",
                         fixture.Recipients.Street,
                         " ", 
                         " "
                     };
        //mix        
        yield return new object[] 
                     {
                         null!, 
                         null!, 
                         fixture.Recipients.FlatNumber, 
                         null!, 
                         null!, 
                         null!, 
                         null!, 
                         fixture.Recipients.Street, 
                         null!, 
                         null!
                      };

        yield return new object[]
                     {
                         "",
                         null!,
                         fixture.Recipients.FlatNumber, 
                         "", 
                         fixture.Recipients.Name, 
                         " ",
                         null!,
                         fixture.Recipients.Street, 
                         "", 
                         " "
                     };
        //mix        
        yield return new object[] 
                     {
                         null!,
                         " ",
                         fixture.Recipients.FlatNumber,
                         fixture.Recipients.LastName, 
                         "", 
                         " ", 
                         " ",
                         fixture.Recipients.Street,
                         null!,
                         ""
        };
    }
}
