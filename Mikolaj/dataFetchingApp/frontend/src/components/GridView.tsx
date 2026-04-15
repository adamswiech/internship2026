import { useEffect, useState } from "react";
import Api from "../../scripts/api";
import type { PersonalDataModel } from "../interfaces/PersonalDataModel";
import GridElement from "./GridElement";

export default function GridView() {
  const [data, setData] = useState<PersonalDataModel[]>([]);
  const [queryProps, setQueryProps] = useState({ limit: 20, offset: 0 });
  const [navigation, setNavigation] = useState(1);
  const [next, setNext] = useState(0);
  const [searchVal, setSearchVal] = useState("");
  const [status, setStatus] = useState("Loading...");
  const [operation, setOperation] = useState("FetchData");

  useEffect(() => {
    if (operation == "FetchData") {
      const fetchData = async () => {
        const response = await Api.fetchData(
          queryProps.offset,
          queryProps.limit,
        );
        setData((prevData) => [...prevData, ...response]);
      };

      fetchData();
      setQueryProps({
        offset: queryProps.offset + 20,
        limit: queryProps.limit,
      });
    } else if (operation == "FilterData") {
      const fetchData = async () => {

        const searchResult = searchVal.split(" ");

        const response = await Api.filterDataByLastName(
          queryProps.offset,
          queryProps.limit,
          searchResult[0], //change it here
          // searchResult[1], //change it here
        );

        setData((prevData) => [...prevData, ...response]);
        setQueryProps({
          offset: queryProps.offset + 20,
          limit: queryProps.limit,
        });
      };

      fetchData();
    }

    setStatus("Data fetched");
  }, [next, operation]);

  const handleSearch = async () => {
    setOperation("FilterData");
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
            Next {">"}
          </button>
          <button
            onClick={() => navigation != 1 && setNavigation(navigation - 1)}
          >
            {"<"} Prev
          </button>
        </div>
      </div>

      <div className="search-bar">
        <input
          onChange={(e) => {
            setSearchVal(e.target.value);
          }}
          type="text"
          placeholder="Search for first or last name..."
        />
        <button onClick={() => handleSearch()}>Search</button>
      </div>
      {status != "Loading..." ? (
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
      ) : (
        status
      )}
    </div>
  );
}
