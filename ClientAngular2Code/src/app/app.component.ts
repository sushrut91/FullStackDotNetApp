import { DepartmentType } from './departmentType.model';
import { DepartmentModel } from './department.model';
import { Component, OnInit, ApplicationRef } from '@angular/core';
import { dataService } from './data.service';
import {Observable} from 'rxjs';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers:[dataService]
})
export class AppComponent implements OnInit{
  private title = 'Departments Menu';
  private result:DepartmentModel[];
  private departmentTypeList:DepartmentType[];
  private deptName:string;
  private noEmp:number;
  private status:number;
  private responseStatus:number
  private departmentModel : DepartmentModel = {DepartmentId:0,DepartmentName:"",NoOfEmployees:0,DepartmentType:1,DepartmentTypeList:null,HasDisabledEmployees:false};
  private appRef:ApplicationRef;
  private showHideForm:boolean=false;
  private showHideList:boolean=false;
  private showSpinner:boolean=true;

  constructor(private ds:dataService) {
      
  }
  ngOnInit(){
  
  }

  AddNewDeptClicked(){
    this.showHideList= false;
    this.showHideForm = true;
    this.getDepartmentTypesList();
  }
  ListAllDeptsClicked(){
    this.showHideForm = false;
    this.showHideList = true;
    this.getDepartmentList();
  }
  getDepartmentList(){
    return this.ds.getAllDepartments()
    .subscribe(response=>this.result=response,
      ()=>this.showSpinner=false,
      ()=>this.showSpinner=false);
  }

  getDepartmentTypesList(){
    return this.ds.getDepartmentTypes()
    .subscribe(response=>this.departmentTypeList = response,
      ()=>this.showSpinner=false,
      ()=>this.showSpinner=false);
  }
  //submit btn clicked
  saveData(data:DepartmentModel){
    this.result.push(data);
    return this.ds.sendDepartmentOnSubmit(data)
    .subscribe(response=>this.status=response,
      ()=>{},
      ()=>this.departmentModel={DepartmentId:0,DepartmentName:""
      ,NoOfEmployees:0,DepartmentType:1,DepartmentTypeList:null
      ,HasDisabledEmployees:false});
  }
}
