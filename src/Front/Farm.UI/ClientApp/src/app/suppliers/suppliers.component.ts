import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';


@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  styleUrls: ['./suppliers.component.css']
})
export class SuppliersComponent {
  public suppliers: Supplier[];
  
  constructor(public dialog: MatDialog, public http: HttpClient, public @Inject('SUPPLIER_URL') baseUrl: string) {

  }

  ngOnInit() {
    this.http.get<Supplier[]>(this.baseUrl + 'supplier').subscribe(result => {
      this.suppliers = result;
    }, error => console.error(error));
  }
  public edit(supplier : Supplier) {

  }
  public add(supplier : Supplier) {
    const dialogRef = this.dialog.open(DialogOverviewExampleDialog, {
      width: '250px',
      data: {supplier: supplier}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
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

@Component({
  selector: 'dialog-overview-example-dialog',
  templateUrl: 'dialog-overview-example-dialog.html',
})
export class DialogOverviewExampleDialog {

  constructor(
    public dialogRef: MatDialogRef<DialogOverviewExampleDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Supplier) {}

  onNoClick(): void {
    this.dialogRef.close();
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
