import { DrugParams } from './../_models/DrugParams';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { of } from 'rxjs';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { HttpClient } from '@angular/common/http';
import { Drug } from '../_models/Drug';

@Injectable({
  providedIn: 'root',
})
export class DrugService {
  getUserParams(): DrugParams {
    throw new Error('Method not implemented.');
  }
  baseUrl = environment.apiUrl + 'drug';
  drugParams: DrugParams;
  drugCache = new Map();

  constructor(private http: HttpClient) {
    this.drugParams = new DrugParams();
  }

  getDrugParams() {
    return this.drugParams;
  }

  setDrugParams(params: DrugParams) {
    this.drugParams = params;
  }

  resetDrugParams() {
    this.drugParams = new DrugParams();
    return this.drugParams;
  }

  getDrugs(drugParams: DrugParams) {
    var response = this.drugCache.get(Object.values(drugParams).join('-'));
    if (response) {
      return of(response);
    }

    let params = getPaginationHeaders(
      drugParams.pageNumber,
      drugParams.pageSize
    );

    params = params.append('pharmGroupId', drugParams.pharmGroupId.toString());
    params = params.append('drugName', drugParams.drugName);
    params = params.append(
      'alphabeticalOrderAsc',
      drugParams.alphabeticalOrderAsc.toString()
    );

    return getPaginatedResult<Drug[]>(this.baseUrl, params, this.http).pipe(
      map((response) => {
        this.drugCache.set(Object.values(drugParams).join('-'), response);
        return response;
      })
    );
  }

  createDrug(drug: any){
    return this.http.post(this.baseUrl, drug);
  }


  editDrug(drug: any, id: any){
    return this.http.put(this.baseUrl + "/" + id, drug);
  }

  removeDrug(id: any){
    return this.http.delete(this.baseUrl + "/" + id);
  }

  getDrugIngredients(id: any){
    return this.http.get(this.baseUrl + "/ingredients/" + id);
  }

  markIncompatibleDrugs(ids: any){
    return this.http.post(this.baseUrl + "/incompatibledrugs/", ids);
  }

  markAllergicDrugs(email: any, ids:any){
    return this.http.post(this.baseUrl + "/allergicdrugs/" + email, ids);
  }



}
