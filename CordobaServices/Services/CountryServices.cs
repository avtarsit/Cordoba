
using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class CountryServices : ICountryServices
    {
        private GenericRepository<CountryEntity> objGenericRepository = new GenericRepository<CountryEntity>();

        public List<CountryEntity> GetCountryList(int countryId)
        {
            List<CountryEntity> Countries = new List<CountryEntity>();
            var paramCountryId = new SqlParameter { ParameterName = "countryId", DbType = DbType.Int32, Value = countryId };

            Countries = objGenericRepository.ExecuteSQL<CountryEntity>("GetCountryList", paramCountryId).ToList();

            //Countries.Add(new CountryEntity() { CountryName = "Andorra ", CountryCd = "AD" });
            //Countries.Add(new CountryEntity() { CountryName = "United Arab Emirates ", CountryCd = "AE " });
            //Countries.Add(new CountryEntity() { CountryName = "Afghanistan ", CountryCd = "AF" });
            //Countries.Add(new CountryEntity() { CountryName = "Antigua and Barbuda ", CountryCd = "AG " });
            //Countries.Add(new CountryEntity() { CountryName = "Albania ", CountryCd = "AL " });
            //Countries.Add(new CountryEntity() { CountryName = "Armenia ", CountryCd = "AM " });
            //Countries.Add(new CountryEntity() { CountryName = "  Angola                                ", CountryCd = " AO" });
            //Countries.Add(new CountryEntity() { CountryName = "  Argentina                             ", CountryCd = " AR" });
            //Countries.Add(new CountryEntity() { CountryName = "Austria                                 ", CountryCd = " AT" });
            //Countries.Add(new CountryEntity() { CountryName = "Australia                               ", CountryCd = " AU" });
            //Countries.Add(new CountryEntity() { CountryName = "Azerbaijan                              ", CountryCd = " AZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Bosnia and Herzegovina                  ", CountryCd = " BA" });
            //Countries.Add(new CountryEntity() { CountryName = "Barbados                                ", CountryCd = " BB" });
            //Countries.Add(new CountryEntity() { CountryName = "Bangladesh                              ", CountryCd = " BD" });
            //Countries.Add(new CountryEntity() { CountryName = "Belgium                                 ", CountryCd = " BE" });
            //Countries.Add(new CountryEntity() { CountryName = "Burkina Faso                            ", CountryCd = " BF" });
            //Countries.Add(new CountryEntity() { CountryName = "Bulgaria                                ", CountryCd = " BG" });
            //Countries.Add(new CountryEntity() { CountryName = "Bahrain                                 ", CountryCd = " BH" });
            //Countries.Add(new CountryEntity() { CountryName = "Burundi                                 ", CountryCd = " BI" });
            //Countries.Add(new CountryEntity() { CountryName = "Benin                                   ", CountryCd = " BJ" });
            //Countries.Add(new CountryEntity() { CountryName = "Brunei Darussalam                       ", CountryCd = " BN" });
            //Countries.Add(new CountryEntity() { CountryName = "Bolivia (Plurinational State of)        ", CountryCd = " BO" });
            //Countries.Add(new CountryEntity() { CountryName = "Brazil                                  ", CountryCd = " BR" });
            //Countries.Add(new CountryEntity() { CountryName = "Bahamas                                 ", CountryCd = " BS" });
            //Countries.Add(new CountryEntity() { CountryName = "Bhutan                                  ", CountryCd = " BT" });
            //Countries.Add(new CountryEntity() { CountryName = "Botswana                                ", CountryCd = " BW" });
            //Countries.Add(new CountryEntity() { CountryName = "Belarus                                 ", CountryCd = " BY" });
            //Countries.Add(new CountryEntity() { CountryName = "Belize                                  ", CountryCd = " BZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Canada                                  ", CountryCd = " CA" });
            //Countries.Add(new CountryEntity() { CountryName = "Democratic Republic of the Congo        ", CountryCd = " CD" });
            //Countries.Add(new CountryEntity() { CountryName = "Central African Republic                ", CountryCd = " CF" });
            //Countries.Add(new CountryEntity() { CountryName = "Congo                                   ", CountryCd = " CG" });
            //Countries.Add(new CountryEntity() { CountryName = "Switzerland                             ", CountryCd = " CH" });
            //Countries.Add(new CountryEntity() { CountryName = "Côte d'Ivoire                           ", CountryCd = " CI" });
            //Countries.Add(new CountryEntity() { CountryName = "Chile                                   ", CountryCd = " CL" });
            //Countries.Add(new CountryEntity() { CountryName = "Cameroon                                ", CountryCd = " CM" });
            //Countries.Add(new CountryEntity() { CountryName = "China                                   ", CountryCd = " CN" });
            //Countries.Add(new CountryEntity() { CountryName = "Colombia                                ", CountryCd = " CO" });
            //Countries.Add(new CountryEntity() { CountryName = "Costa Rica                              ", CountryCd = " CR" });
            //Countries.Add(new CountryEntity() { CountryName = "Cuba                                    ", CountryCd = " CU" });
            //Countries.Add(new CountryEntity() { CountryName = "Cape Verde                              ", CountryCd = " CV" });
            //Countries.Add(new CountryEntity() { CountryName = "Cyprus                                  ", CountryCd = " CY" });
            //Countries.Add(new CountryEntity() { CountryName = "Czech Republic                          ", CountryCd = " CZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Germany                                 ", CountryCd = " DE" });
            //Countries.Add(new CountryEntity() { CountryName = "Djibouti                                ", CountryCd = " DJ" });
            //Countries.Add(new CountryEntity() { CountryName = "Denmark                                 ", CountryCd = " DK" });
            //Countries.Add(new CountryEntity() { CountryName = "Dominica                                ", CountryCd = " DM" });
            //Countries.Add(new CountryEntity() { CountryName = "Dominican Republic                      ", CountryCd = " DO" });
            //Countries.Add(new CountryEntity() { CountryName = "Algeria                                 ", CountryCd = " DZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Ecuador                                 ", CountryCd = " EC" });
            //Countries.Add(new CountryEntity() { CountryName = "Estonia                                 ", CountryCd = " EE" });
            //Countries.Add(new CountryEntity() { CountryName = "Egypt                                   ", CountryCd = " EG" });
            //Countries.Add(new CountryEntity() { CountryName = "Eritrea                                 ", CountryCd = " ER" });
            //Countries.Add(new CountryEntity() { CountryName = "Spain                                   ", CountryCd = " ES" });
            //Countries.Add(new CountryEntity() { CountryName = "Ethiopia                                ", CountryCd = " ET" });
            //Countries.Add(new CountryEntity() { CountryName = "Finland                                 ", CountryCd = " FI" });
            //Countries.Add(new CountryEntity() { CountryName = "Fiji                                    ", CountryCd = " FJ" });
            //Countries.Add(new CountryEntity() { CountryName = "Micronesia (Federated States of)        ", CountryCd = " FM" });
            //Countries.Add(new CountryEntity() { CountryName = "France                                  ", CountryCd = " FR" });
            //Countries.Add(new CountryEntity() { CountryName = "Gabon                                   ", CountryCd = " GA" });
            //Countries.Add(new CountryEntity() { CountryName = "United Kingdom of Great Britain and N   ", CountryCd = " GB" });
            //Countries.Add(new CountryEntity() { CountryName = "Grenada                                 ", CountryCd = " GD" });
            //Countries.Add(new CountryEntity() { CountryName = "Georgia                                 ", CountryCd = " GE" });
            //Countries.Add(new CountryEntity() { CountryName = "Ghana                                   ", CountryCd = " GH" });
            //Countries.Add(new CountryEntity() { CountryName = "Gambia                                  ", CountryCd = " GM" });
            //Countries.Add(new CountryEntity() { CountryName = "Guinea                                  ", CountryCd = " GN" });
            //Countries.Add(new CountryEntity() { CountryName = "Equatorial Guinea                       ", CountryCd = " GQ" });
            //Countries.Add(new CountryEntity() { CountryName = "Greece                                  ", CountryCd = " GR" });
            //Countries.Add(new CountryEntity() { CountryName = "Guatemala                               ", CountryCd = " GT" });
            //Countries.Add(new CountryEntity() { CountryName = "Guinea-Bissau                           ", CountryCd = " GW" });
            //Countries.Add(new CountryEntity() { CountryName = "Guyana                                  ", CountryCd = " GY" });
            //Countries.Add(new CountryEntity() { CountryName = "Honduras                                ", CountryCd = " HN" });
            //Countries.Add(new CountryEntity() { CountryName = "Croatia                                 ", CountryCd = " HR" });
            //Countries.Add(new CountryEntity() { CountryName = "Haiti                                   ", CountryCd = " HT" });
            //Countries.Add(new CountryEntity() { CountryName = "Hungary                                 ", CountryCd = " HU" });
            //Countries.Add(new CountryEntity() { CountryName = "Indonesia                               ", CountryCd = " ID" });
            //Countries.Add(new CountryEntity() { CountryName = "Ireland                                 ", CountryCd = " IE" });
            //Countries.Add(new CountryEntity() { CountryName = "Israel                                  ", CountryCd = " IL" });
            //Countries.Add(new CountryEntity() { CountryName = "India                                   ", CountryCd = " IN" });
            //Countries.Add(new CountryEntity() { CountryName = "Iraq                                    ", CountryCd = " IQ" });
            //Countries.Add(new CountryEntity() { CountryName = "Iran (Islamic Republic of)              ", CountryCd = " IR" });
            //Countries.Add(new CountryEntity() { CountryName = "Iceland                                 ", CountryCd = " IS" });
            //Countries.Add(new CountryEntity() { CountryName = "Italy                                   ", CountryCd = " IT" });
            //Countries.Add(new CountryEntity() { CountryName = "Jamaica                                 ", CountryCd = " JM" });
            //Countries.Add(new CountryEntity() { CountryName = "Jordan                                  ", CountryCd = " JO" });
            //Countries.Add(new CountryEntity() { CountryName = "Japan                                   ", CountryCd = " JP" });
            //Countries.Add(new CountryEntity() { CountryName = "Kenya                                   ", CountryCd = " KE" });
            //Countries.Add(new CountryEntity() { CountryName = "Kyrgyzstan                              ", CountryCd = " KG" });
            //Countries.Add(new CountryEntity() { CountryName = "Cambodia                                ", CountryCd = " KH" });
            //Countries.Add(new CountryEntity() { CountryName = "Kiribati                                ", CountryCd = " KI" });
            //Countries.Add(new CountryEntity() { CountryName = "Comoros                                 ", CountryCd = " KM" });
            //Countries.Add(new CountryEntity() { CountryName = "Saint Kitts and Nevis                   ", CountryCd = " KN" });
            //Countries.Add(new CountryEntity() { CountryName = "Democratic People's Republic of Korea   ", CountryCd = " KP" });
            //Countries.Add(new CountryEntity() { CountryName = "Republic of Korea                       ", CountryCd = " KR" });
            //Countries.Add(new CountryEntity() { CountryName = "Kuwait                                  ", CountryCd = " KW" });
            //Countries.Add(new CountryEntity() { CountryName = "Kazakhstan                              ", CountryCd = " KZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Lao People's Democratic Republic        ", CountryCd = " LA" });
            //Countries.Add(new CountryEntity() { CountryName = "Lebanon                                 ", CountryCd = " LB" });
            //Countries.Add(new CountryEntity() { CountryName = "Saint Lucia                             ", CountryCd = " LC" });
            //Countries.Add(new CountryEntity() { CountryName = "Liechtenstein                           ", CountryCd = " LI" });
            //Countries.Add(new CountryEntity() { CountryName = "Sri Lanka                               ", CountryCd = " LK" });
            //Countries.Add(new CountryEntity() { CountryName = "Liberia                                 ", CountryCd = " LR" });
            //Countries.Add(new CountryEntity() { CountryName = "Lesotho                                 ", CountryCd = " LS" });
            //Countries.Add(new CountryEntity() { CountryName = "Lithuania                               ", CountryCd = " LT" });
            //Countries.Add(new CountryEntity() { CountryName = "Luxembourg                              ", CountryCd = " LU" });
            //Countries.Add(new CountryEntity() { CountryName = "Latvia                                  ", CountryCd = " LV" });
            //Countries.Add(new CountryEntity() { CountryName = "Libyan Arab Jamahiriya                  ", CountryCd = " LY" });
            //Countries.Add(new CountryEntity() { CountryName = "Morocco                                 ", CountryCd = " MA" });
            //Countries.Add(new CountryEntity() { CountryName = "Monaco                                  ", CountryCd = " MC" });
            //Countries.Add(new CountryEntity() { CountryName = "Republic of Moldova                     ", CountryCd = " MD" });
            //Countries.Add(new CountryEntity() { CountryName = "Montenegro                              ", CountryCd = " ME" });
            //Countries.Add(new CountryEntity() { CountryName = "Madagascar                              ", CountryCd = " MG" });
            //Countries.Add(new CountryEntity() { CountryName = "Marshall Islands                        ", CountryCd = " MH" });
            //Countries.Add(new CountryEntity() { CountryName = "The former Yugoslav Republic of Maced   ", CountryCd = " MK" });
            //Countries.Add(new CountryEntity() { CountryName = "Mali                                    ", CountryCd = " ML" });
            //Countries.Add(new CountryEntity() { CountryName = "Myanmar                                 ", CountryCd = " MM" });
            //Countries.Add(new CountryEntity() { CountryName = "Mongolia                                ", CountryCd = " MN" });
            //Countries.Add(new CountryEntity() { CountryName = "Mauritania                              ", CountryCd = " MR" });
            //Countries.Add(new CountryEntity() { CountryName = "Malta                                   ", CountryCd = " MT" });
            //Countries.Add(new CountryEntity() { CountryName = "Mauritius                               ", CountryCd = " MU" });
            //Countries.Add(new CountryEntity() { CountryName = "Maldives                                ", CountryCd = " MV" });
            //Countries.Add(new CountryEntity() { CountryName = "Malawi                                  ", CountryCd = " MW" });
            //Countries.Add(new CountryEntity() { CountryName = "Mexico                                  ", CountryCd = " MX" });
            //Countries.Add(new CountryEntity() { CountryName = "Malaysia                                ", CountryCd = " MY" });
            //Countries.Add(new CountryEntity() { CountryName = "Mozambique                              ", CountryCd = " MZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Namibia                                 ", CountryCd = " NA" });
            //Countries.Add(new CountryEntity() { CountryName = "Niger                                   ", CountryCd = " NE" });
            //Countries.Add(new CountryEntity() { CountryName = "Nigeria                                 ", CountryCd = " NG" });
            //Countries.Add(new CountryEntity() { CountryName = "Nicaragua                               ", CountryCd = " NI" });
            //Countries.Add(new CountryEntity() { CountryName = "Netherlands                             ", CountryCd = " NL" });
            //Countries.Add(new CountryEntity() { CountryName = "Norway                                  ", CountryCd = " NO" });
            //Countries.Add(new CountryEntity() { CountryName = "Nepal                                   ", CountryCd = " NP" });
            //Countries.Add(new CountryEntity() { CountryName = "Nauru                                   ", CountryCd = " NR" });
            //Countries.Add(new CountryEntity() { CountryName = "New Zealand                             ", CountryCd = " NZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Oman                                    ", CountryCd = " OM" });
            //Countries.Add(new CountryEntity() { CountryName = "Panama                                  ", CountryCd = " PA" });
            //Countries.Add(new CountryEntity() { CountryName = "Peru                                    ", CountryCd = " PE" });
            //Countries.Add(new CountryEntity() { CountryName = "Papua New Guinea                        ", CountryCd = " PG" });
            //Countries.Add(new CountryEntity() { CountryName = "Philippines                             ", CountryCd = " PH" });
            //Countries.Add(new CountryEntity() { CountryName = "Pakistan                                ", CountryCd = " PK" });
            //Countries.Add(new CountryEntity() { CountryName = "Poland                                  ", CountryCd = " PL" });
            //Countries.Add(new CountryEntity() { CountryName = "Portugal                                ", CountryCd = " PT" });
            //Countries.Add(new CountryEntity() { CountryName = "Palau                                   ", CountryCd = " PW" });
            //Countries.Add(new CountryEntity() { CountryName = "Paraguay                                ", CountryCd = " PY" });
            //Countries.Add(new CountryEntity() { CountryName = "Qatar                                   ", CountryCd = " QA" });
            //Countries.Add(new CountryEntity() { CountryName = "Romania                                 ", CountryCd = " RO" });
            //Countries.Add(new CountryEntity() { CountryName = "Serbia                                  ", CountryCd = " RS" });
            //Countries.Add(new CountryEntity() { CountryName = "Russian Federation                      ", CountryCd = " RU" });
            //Countries.Add(new CountryEntity() { CountryName = "Rwanda                                  ", CountryCd = " RW" });
            //Countries.Add(new CountryEntity() { CountryName = "Saudi Arabia                            ", CountryCd = " SA" });
            //Countries.Add(new CountryEntity() { CountryName = "Solomon Islands                         ", CountryCd = " SB" });
            //Countries.Add(new CountryEntity() { CountryName = "Seychelles                              ", CountryCd = " SC" });
            //Countries.Add(new CountryEntity() { CountryName = "Sudan                                   ", CountryCd = " SD" });
            //Countries.Add(new CountryEntity() { CountryName = "Sweden                                  ", CountryCd = " SE" });
            //Countries.Add(new CountryEntity() { CountryName = "Singapore                               ", CountryCd = " SG" });
            //Countries.Add(new CountryEntity() { CountryName = "Slovenia                                ", CountryCd = " SI" });
            //Countries.Add(new CountryEntity() { CountryName = "Slovakia                                ", CountryCd = " SK" });
            //Countries.Add(new CountryEntity() { CountryName = "Sierra Leone                            ", CountryCd = " SL" });
            //Countries.Add(new CountryEntity() { CountryName = "San Marino                              ", CountryCd = " SM" });
            //Countries.Add(new CountryEntity() { CountryName = "Senegal                                 ", CountryCd = " SN" });
            //Countries.Add(new CountryEntity() { CountryName = "Somalia                                 ", CountryCd = " SO" });
            //Countries.Add(new CountryEntity() { CountryName = "Suriname                                ", CountryCd = " SR" });
            //Countries.Add(new CountryEntity() { CountryName = "South Sudan                             ", CountryCd = " SS" });
            //Countries.Add(new CountryEntity() { CountryName = "Sao Tome and Principe                   ", CountryCd = " ST" });
            //Countries.Add(new CountryEntity() { CountryName = "El Salvador                             ", CountryCd = " SV" });
            //Countries.Add(new CountryEntity() { CountryName = "Syrian Arab Republic                    ", CountryCd = " SY" });
            //Countries.Add(new CountryEntity() { CountryName = "Swaziland                               ", CountryCd = " SZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Chad                                    ", CountryCd = " TD" });
            //Countries.Add(new CountryEntity() { CountryName = "Togo                                    ", CountryCd = " TG" });
            //Countries.Add(new CountryEntity() { CountryName = "Thailand                                ", CountryCd = " TH" });
            //Countries.Add(new CountryEntity() { CountryName = "Tajikistan                              ", CountryCd = " TJ" });
            //Countries.Add(new CountryEntity() { CountryName = "Timor-Leste                             ", CountryCd = " TL" });
            //Countries.Add(new CountryEntity() { CountryName = "Turkmenistan                            ", CountryCd = " TM" });
            //Countries.Add(new CountryEntity() { CountryName = "Tunisia                                 ", CountryCd = " TN" });
            //Countries.Add(new CountryEntity() { CountryName = "Tonga                                   ", CountryCd = " TO" });
            //Countries.Add(new CountryEntity() { CountryName = "Turkey                                  ", CountryCd = " TR" });
            //Countries.Add(new CountryEntity() { CountryName = "Trinidad and Tobago                     ", CountryCd = " TT" });
            //Countries.Add(new CountryEntity() { CountryName = "Tuvalu                                  ", CountryCd = " TV" });
            //Countries.Add(new CountryEntity() { CountryName = "United Republic of Tanzania             ", CountryCd = " TZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Ukraine                                 ", CountryCd = " UA" });
            //Countries.Add(new CountryEntity() { CountryName = "Uganda                                  ", CountryCd = " UG" });
            //Countries.Add(new CountryEntity() { CountryName = "United States of America                ", CountryCd = " US" });
            //Countries.Add(new CountryEntity() { CountryName = "Uruguay                                 ", CountryCd = " UY" });
            //Countries.Add(new CountryEntity() { CountryName = "Uzbekistan                              ", CountryCd = " UZ" });
            //Countries.Add(new CountryEntity() { CountryName = "Saint Vincent and the Grenadines        ", CountryCd = " VC" });
            //Countries.Add(new CountryEntity() { CountryName = "Venezuela (Bolivarian Republic of)      ", CountryCd = " VE" });
            //Countries.Add(new CountryEntity() { CountryName = "Viet Nam                                ", CountryCd = " VN" });
            //Countries.Add(new CountryEntity() { CountryName = "Vanuatu                                 ", CountryCd = " VU" });
            //Countries.Add(new CountryEntity() { CountryName = "Samoa                                   ", CountryCd = " WS" });
            //Countries.Add(new CountryEntity() { CountryName = "Yemen                                   ", CountryCd = " YE" });
            //Countries.Add(new CountryEntity() { CountryName = "South Africa                            ", CountryCd = " ZA" });
            //Countries.Add(new CountryEntity() { CountryName = "Zambia                                  ", CountryCd = " ZM" });
            //Countries.Add(new CountryEntity() { CountryName = "Zimbabwe                                ", CountryCd = " ZW" });




            return Countries;




        }

        public int InsertOrUpdateCountry(CountryEntity objCountry)
        {
            var paramCountryId = new SqlParameter { ParameterName = "countryId", DbType = DbType.Int32, Value = Convert.ToInt32(objCountry.CountryId) };
            var paramCountryCd = new SqlParameter { ParameterName = "countryCd", DbType = DbType.String, Value = objCountry.CountryCd ?? (object)DBNull.Value };
            var paramCountryName = new SqlParameter { ParameterName = "countryName", DbType = DbType.String, Value = objCountry.CountryName ?? (object)DBNull.Value };
            var paramstatus = new SqlParameter { ParameterName = "status", DbType = DbType.Int32, Value = objCountry.Status };

            int result = objGenericRepository.ExecuteSQL<int>("InsertOrUpdateCountry", paramCountryId, paramCountryCd, paramCountryName, paramstatus).FirstOrDefault();
            return result;
        }
    }
}
