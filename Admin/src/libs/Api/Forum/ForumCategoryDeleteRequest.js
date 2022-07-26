import WebServiceRequest from '../../Api/WebServiceRequest'

class ForumCategoryDeleteRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('ForumCategory/Delete?id='+id)
  }
}
export default ForumCategoryDeleteRequest;
