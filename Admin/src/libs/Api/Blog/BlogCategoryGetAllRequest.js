import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCategoryGetAllRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('BlogCategory/GetAll')
  }
}
export default BlogCategoryGetAllRequest;
