import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCommentGetAllByBlogIdRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('BlogComment/GetAllByBlogId?blogId='+id)
  }
}
export default BlogCommentGetAllByBlogIdRequest;
