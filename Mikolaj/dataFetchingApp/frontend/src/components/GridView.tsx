import { useEffect, useState } from "react";
import Api from "../../scripts/api";
import type { PersonalDataModel } from "../interfaces/PersonalDataModel";
import GridElement from "./GridElement";

export default function GridView() {
  const [data, setData] = useState<PersonalDataModel[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      const response = await Api.fetchData();
      console.log(response);
      setData(response);
    };

    fetchData();
  }, []);

  return (
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

      {data.map((element: PersonalDataModel) => (
        <GridElement key={element.id} element={element} />
      ))}
    </table>
  );
}
