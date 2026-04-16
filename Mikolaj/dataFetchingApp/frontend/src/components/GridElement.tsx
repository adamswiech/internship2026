import type { PersonalDataModel } from "../interfaces/PersonalDataModel";

interface GridElement {
  element: PersonalDataModel;
}

export default function GridElement({ element }: GridElement) {
  return (
    <tr>
      <td>{element.id}</td>
      <td>{element.firstName}</td>
      <td>{element.lastName}</td>
      <td>{element.age}</td>
      <td>{element.gender}</td>
      <td>{element.phoneNumber}</td>
      <td>{element.emailAddress}</td>
      <td>{element.postCode}</td>
      <td>{element.city}</td>
      <td>{element.country}</td>
    </tr>
  );
}
