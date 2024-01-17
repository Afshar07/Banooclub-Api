import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogSeoUpdateRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('Blog/UpdateBlogSEO')
  }
  setParams(data){
    super.setRequestParamDataObj(data)
  }
}
export default BlogSeoUpdateRequest;
