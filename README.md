﻿# Mine Sweeper Roguelike

---

Roguelike 扫雷

> Powered by Unity 2021 LTS

## Todo

- [ ] 揭开状态下正确渲染格子
    - [x] 周围没有炸弹时显示空格子
    - [x] 周围有炸弹但自己没有时显示周围炸弹数量
    - [x] 自己有炸弹时显示炸弹
        - [ ] 如果是插旗后揭开有炸弹，打叉
- [x] 递归揭开格子
- [x] 判断胜负
- [x] 重构MineGrid，分离出部分功能
    - [x] 分离计算邻居相关的功能至IGridBrain接口
    - [x] 分离玩家输入相关的逻辑至单独的类
- [ ] 类似Doom的作弊码系统
- [ ] 支持实时切换是否显示动画
- [ ] UI
    - [x] 实时显示剩余未开启数目
    - [ ] 重开按钮
    - [ ] 调整地图大小的控件
- [ ] 计时模式
    - [ ] 时间快到的时候有心跳声
    - [ ] 动画速度和受时间影响
- [ ] 可替换的雷区背景
- [ ] 修复同时按左右键会在空地上插旗的bug
