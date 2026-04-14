import { useEffect, useState } from "react";
import Api from "../../scripts/api";
import type { PersonalDataModel } from "../interfaces/PersonalDataModel";
import GridElement from "./GridElement";

export default function GridView() {
  const [data, setData] = useState<PersonalDataModel[]>([]);
  const [queryProps, setQueryProps] = useState({ limit: 20, offset: 0 });

  const [searchData, setSearchData] = useState({ firstName: "", lastName: "" });

  const [tmp, setTmp] = useState("");

  const [navigation, setNavigation] = useState(1);
  const [next, setNext] = useState(0);

  useEffect(() => {
    const fetchData = async () => {
      const response = await Api.fetchData(queryProps.offset, queryProps.limit);
      //   console.log(response);
      setData((prevData) => [...prevData, ...response]);
    };

    fetchData();
    setQueryProps({
      offset: queryProps.offset + 20,
      limit: queryProps.limit,
    });
  }, [next]);

  const handleSearch = async () => {
    console.log("search");

    const [firstName, lastName] = tmp.trim().split(/\s+/);

    setSearchData({
      firstName: firstName || "", // first part
      lastName: lastName || "", // second part
    });

    const response = await Api.filterData(
      queryProps.offset,
      queryProps.limit,
      searchData.firstName,
      searchData.lastName,
    );

    console.log(response); //here continue tomorrow 
  };

  return (
    <div className="app-box">
      <div className="navigation">
        <p>Site number: {navigation}</p>
        <div className="navigation-buttons-box">
          <button
            onClick={() => {
              setNavigation(navigation + 1);
              setNext(navigation);
            }}
          >
            Next
          </button>
          <button
            onClick={() => navigation != 1 && setNavigation(navigation - 1)}
          >
            Prev
          </button>
        </div>
      </div>

      <div className="search-bar">
        <input
          onChange={(e) => {
            setTmp(e.target.value);
            console.log(e.target.value);
          }}
          type="text"
          placeholder="Search for first or last name..."
        />
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
          {data
            .slice(navigation * 20, navigation * 20 + 20)
            .map((element: PersonalDataModel) => (
              <GridElement key={element.id} element={element} />
            ))}
        </tbody>
      </table>
    </div>
  );
}
