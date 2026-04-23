import type { Person, PagedDTO } from "../Interfaces/people";

const BASE_URL = "http://localhost:5124/api";

export async function getPeople(params: {
  page: number;
  pageSize: number;
  firstName?: string;
  lastName?: string;
  orderBy?: string;
  reqPages?: boolean;
}): Promise<PagedDTO<Person>> {
  const url = new URL(`${BASE_URL}/people`);
  url.searchParams.append("page", params.page.toString());
  url.searchParams.append("pageSize", params.pageSize.toString());
  
  if (params.reqPages) {
    url.searchParams.append("reqPages", "true");
  }
  if (params.firstName) {
    url.searchParams.append("firstName", params.firstName);
  }

  if (params.lastName) {
    url.searchParams.append("lastName", params.lastName);
  }
  if (params.orderBy) {
    url.searchParams.append("orderBy", params.orderBy);
  }
  const res = await fetch(url.toString());
  if (!res.ok) {
    throw new Error(`HTTP error! status: ${res.status}`);
  }
  console.log(res);
  return res.json();
}