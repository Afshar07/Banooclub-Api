import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCategoryCreateRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('BlogCategory/CreateBlogCategory')
  }
  setParams(data){
    super.setRequestParamDataObj(data)
  }
}
export default BlogCategoryCreateRequest;
