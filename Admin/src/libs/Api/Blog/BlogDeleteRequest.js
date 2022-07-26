import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogDeleteRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('Blog/DeleteBlog?BlogId='+id)
  }
}
export default BlogDeleteRequest;
