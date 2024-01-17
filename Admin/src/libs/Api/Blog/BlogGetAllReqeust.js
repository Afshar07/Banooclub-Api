import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogGetAllRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag(`Blog/GetAll?tagId=${data.tagId}&pageNumber=${data.pageNumber}&count=${data.count}&searchByTitle=${data.search}`)
  }
}
export default BlogGetAllRequest;
