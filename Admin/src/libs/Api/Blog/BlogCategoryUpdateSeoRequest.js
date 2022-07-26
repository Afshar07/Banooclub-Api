import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCategoryUpdateSeoRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('BlogCategory/UpdateBlogCategorySEO')
  }
  setParams(data){
    super.setRequestParamDataObj(data)
  }
}
export default BlogCategoryUpdateSeoRequest;
