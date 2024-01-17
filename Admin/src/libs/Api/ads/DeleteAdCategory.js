import WebServiceRequest from '../../Api/WebServiceRequest'

class DeleteAdCategory extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag('AdsCategory/Delete?id='+data.id)
  }
}
export default DeleteAdCategory;
