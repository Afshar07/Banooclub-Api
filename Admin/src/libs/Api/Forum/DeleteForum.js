import WebServiceRequest from '../../Api/WebServiceRequest'

class DeleteForum extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('Forum/Delete?id='+id)
  }
}
export default DeleteForum;
