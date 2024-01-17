import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogTagDeleteRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setId(id){
    super.setTag('Tag/Delete?id='+id)
  }
}
export default BlogTagDeleteRequest;
