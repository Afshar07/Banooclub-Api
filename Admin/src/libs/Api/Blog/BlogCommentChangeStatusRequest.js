import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCommentChangeStatusRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('BlogComment/ChangeCommentActivation?blogCommentId='+id)
  }
}
export default BlogCommentChangeStatusRequest;
