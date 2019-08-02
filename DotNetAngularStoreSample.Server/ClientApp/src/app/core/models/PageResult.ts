export class PageResult<T> {
  pageCount: number;
  pageSize: number;
  itemsCount: number;
  pageNumber: number;
  result: T[];
}
