import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  styleUrls: ['./suppliers.component.css']
})
export class SuppliersComponent {
  public suppliers: Supplier[];
  private http : HttpClient;
  baseUrl: string;
  
  constructor(http: HttpClient, @Inject('SUPPLIER_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<Supplier[]>(this.baseUrl + 'supplier').subscribe(result => {
      this.suppliers = result;
    }, error => console.error(error));
  }
  public edit(supplier : Supplier) {

  }
  public add(supplier : Supplier) {

  }

  public delete(supplier : Supplier) {
    // const options = {
    //   headers: new HttpHeaders({
    //     'Content-Type': 'application/json',
    //   }),
    //   body: supplier.id
    // };
    this.http.delete(this.baseUrl + 'supplier?id=' +supplier.id).subscribe(result => {
      this.ngOnInit();
    }, error => console.error(error));
  }
}

interface Supplier  {
  id: number;
  shortName: string;
  longName: string;
  addressLine1: string;
  zipCode: string;
  city: string;
}
