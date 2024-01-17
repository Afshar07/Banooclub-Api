import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCreateTagRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
    super.setTag('Tag/Create')
  }
  setParams(data){
    super.setRequestParamDataObj(data)
  }
}
export default BlogCreateTagRequest;
