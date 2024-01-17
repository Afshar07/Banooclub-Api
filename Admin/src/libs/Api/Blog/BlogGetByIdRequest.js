import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogGetByIdRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('Blog/GetById?id='+id)
  }
}
export default BlogGetByIdRequest;
