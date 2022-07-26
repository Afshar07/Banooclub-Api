import WebServiceRequest from '../../Api/WebServiceRequest'

class DeletePostNk extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag('PostNk/Delete?id='+data.id)
  }
}
export default DeletePostNk;
