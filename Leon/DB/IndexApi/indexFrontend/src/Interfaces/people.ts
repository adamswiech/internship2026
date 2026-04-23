export interface Person {
  id: number;
  first_name: string;
  middle_name: string;
  last_name: string;
  age: number;
  height_cm: number;
  weight_kg: number;
  city: string;
  country: string;
  favorite_number: number;
}

export interface PagedDTO<T> {
  items: T[];
  totalCount: number;
  totalPages: number;
}