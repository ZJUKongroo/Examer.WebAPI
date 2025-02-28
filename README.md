# Examer ACEE 试题提交系统

## Api 文档地址
[OpenApi](https://localhost:7048/openapi/Examer.json)
[Scalar](https://localhost:7048/scalar/Examer.json)

若要启用https，请使用以下命令添加开发证书
```bash
dotnet dev-certs https --trust
```

## 说明
1. 目前开发使用的是Sqlite数据库（为了开发方便），计划正式版本使用SqlServer Express
2. 题目上传的文件夹在[这里](Services/ProblemRepository.cs)第5行，请手动更改目录，后续会将这个字段写入配置文件
3. 分页参数见响应标头 X-Pagination 默认为第1页，每页10条，最多一页20条，如果想要更改请@Daimolean
4. 可以进行任意参数混合筛选 通过路由query参数 详见API文档
5. 枚举值见Enum文件夹下各文件，默认从0开始，枚举值为Null=0是表示不进行筛选，请不要使用Null值
6. 数据库设计我已经尽力了，改了好几天了，有更好的方案请@Daimolean
7. 欢迎提供测试用数据库样本
8. 可能有很多没有考虑到的问题，有很多bugssssssss，发现500 Internal Server Error请@Daimolean

## Todo
1. 获取用户所在群组的考试 根据用户获取队伍 考试的继承
2. 用户是否淘汰的筛选
3. 外键约束
4. 级联删除的实现
6. CreatedAtRoute方法改进
7. 文件类型的校验和文件夹的创建和删除
