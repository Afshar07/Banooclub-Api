import WebServiceRequest from '../../Api/WebServiceRequest'

class ForumCategoryGetAll extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag(`ForumCategory/GetAll?pageNumber=${data.pageNumber}&count=10&searchCommand=${data.Search}`)
  }
}
export default ForumCategoryGetAll;
