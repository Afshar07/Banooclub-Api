import WebServiceRequest from '../../Api/WebServiceRequest'

class CreateBlogCommentRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('BlogComment/CreateBlogComment')
  }
  setBlogId(blogId){
    super.setRequestParam('blogId',blogId)
  }
  setMessage(message){
    super.setRequestParam('message',message)
  }
  setParentId(parentId){
    super.setRequestParam('parentId',parentId)
  }

}
export default CreateBlogCommentRequest;
