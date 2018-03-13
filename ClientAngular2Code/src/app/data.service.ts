import { DepartmentType } from './departmentType.model';
import {Injectable} from '@angular/core';
import {Http,Request,Response,RequestOptions,Headers} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import {DepartmentModel} from './department.model';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class dataService{
    private sendDepartmentModel:DepartmentModel;
    
    constructor(private http:Http) {        
    }
    getAllDepartments():Observable<DepartmentModel[]>{
        return this.http.get("http://localhost:60725/api/departments/").
        map((response:Response)=><DepartmentModel[]>response.json()).
        catch(this.errorHandler);      
    }

    getDepartmentTypes():Observable<DepartmentType[]>{
        return this.http.get("http://localhost:60725/api/departments/departmentTypes")
        .map((response:Response)=><DepartmentType[]>response.json())
        .catch(this.errorHandler);
    }
    sendDepartmentOnSubmit(departmentModel:DepartmentModel):Observable<number>{
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let url="http://localhost:60725/api/departments/add";
        
        return this.http.post(url,departmentModel,options)
        .map((response:Response)=>response.status).
        catch(this.errorHandler);
    }
    errorHandler(error:Response){
        return Observable.throw(error);
    }
}