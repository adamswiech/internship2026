import { useEffect, useState } from "react";
import Api from "../../scripts/api";
import type { PersonalDataModel } from "../interfaces/PersonalDataModel";
import GridElement from "./GridElement";

export default function GridView() {
  const [data, setData] = useState<PersonalDataModel[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      const response = await Api.fetchData();
      setData(response);
    };

    fetchData();
  }, []);

  const handleSearch = () => {
    console.log("search");
  };

  return (
    <div className="app-box">
      <div className="search-bar">
        <input type="text" placeholder="Search for first or last name..." />
        <button onClick={() => handleSearch()}>Search</button>
      </div>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>FirstName</th>
            <th>LastName</th>
            <th>Age</th>
            <th>Gender</th>
            <th>PhoneNumber</th>
            <th>EmailAddress</th>
            <th>PostCode</th>
            <th>City</th>
            <th>Country</th>
          </tr>
        </thead>

        <tbody>
          {data.map((element: PersonalDataModel) => (
            <GridElement key={element.id} element={element} />
          ))}
        </tbody>
      </table>
    </div>
  );
}
