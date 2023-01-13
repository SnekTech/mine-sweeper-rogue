﻿# Mine Sweeper Roguelike

---

Roguelike 扫雷

> Powered by Unity 2021 LTS

## 使用以下开源项目

- [UniTask](https://github.com/Cysharp/UniTask)
- [凤凰点阵体](https://timothyqiu.itch.io/vonwaon-bitmap)

## Todo

- [x] 揭开状态下正确渲染格子
    - [x] 周围没有炸弹时显示空格子
    - [x] 周围有炸弹但自己没有时显示周围炸弹数量
    - [x] 自己有炸弹时显示炸弹
- [ ] 双击或双键点击一个标够旗的数字格子，揭开周围的格子
    - [x] 双击
    - [ ] 双键点击
- [x] 按键松开才开始操作
~~- [ ] 如果是插旗后揭开有炸弹，打叉~~
- [x] 递归揭开格子
- [x] 判断胜负
- [x] 重构MineGrid，分离出部分功能
    - [x] 分离计算邻居相关的功能至IGridBrain接口
    - [x] 分离玩家输入相关的逻辑至单独的类
- [ ] 类似Doom的作弊码系统
- [ ] 支持实时切换是否显示动画
- [ ] UI
    - [x] 实时显示剩余未开启数目
    - [x] 重开按钮
    - [x] 预设不同难度
    - [ ] 梳理交互流程
    - [x] 消耗护甲或生命显示动画
- [ ] 计时模式
    - [ ] 时间快到的时候有心跳声
    - [ ] 动画速度受时间影响
- [ ] 可替换的雷区背景
- [ ] 修复同时按左右键会在空地上插旗的bug
- [ ] 反向扫雷，尽快找出所有地雷
- [x] 道具池SO使用脚本初始化替代手工拖拽
- [ ] 随机种子
~~- [ ] 弹出菜单时背景模糊~~
- [x] 变更雷区大小时自动定位
- [x] 通用的Modal组件
    - [x] 弹出窗口时屏蔽鼠标点击
- [x] 高亮悬停单元
    - [x] 实现一个多扫道具，类似放大镜
- [ ] 彩蛋
    - [ ] 开发者吐槽
- [ ] 道具机制
    - [x] tooltip
    - [ ] 复仇
    - [x] 放大扫雷范围
    - [ ] 影响道具可选数量
    - [ ] 接下来的几次操作有特殊效果
        - [x] 受伤：每次操作减少生命值，持续数次
        - [ ] 踩雷加生命值
        - [ ] 不踩雷加生命值
        - [ ] 延长计时器时间
    - [ ] 透视镜
    - [ ] 一次性道具
        - [ ] 治疗
        - [x] 加最大生命值
        - [ ] 加护甲
        - [ ] 受伤
    - [ ] 蓄力
- [x] 开始界面
- [x] 游戏模式
    - [x] 生命值耗尽
    - [x] 计时模式
- [x] 结束界面 -> 回到开始界面
- [ ] 存档系统
    - [ ] 存档界面
    - [ ] 多个存档
- [ ] 生成雷区使用过程动画
    - [ ] 计时模式下给出3 2 1倒计时后开始
- [ ] 计时模式的时间可变，每一关随机赋值
- [x] 最大血量
  - [x] 血条
- [x] 历史记录
- [ ] 引用字体：[凤凰点阵体](https://timothyqiu.itch.io/vonwaon-bitmap)

```plantuml
left to right direction
User ---> (start)
User ---> (start2)
```

```mermaid
flowchart LR
    id
```

## 状态图

```mermaid
%%{init: {'theme':'neutral'}}%%
stateDiagram-v2
    state 单元格盖子 {
        coveredIdle : 被覆盖
        revealedIdle : 被揭开
        reveal : 揭开
        putCover : 覆盖
        any: 任意状态
        
        [*] --> coveredIdle
        coveredIdle --> reveal : 收到揭开信号
        reveal --> revealedIdle : 揭开动画结束
        revealedIdle --> putCover : 收到覆盖指令
        putCover --> coveredIdle : 覆盖动画结束
        any --> [*] : 本轮游戏结束
    }
```


```mermaid
%%{init: {'theme':'neutral'}}%%
stateDiagram-v2
    state 单元格旗帜 {
        float: 漂浮
        hide: 隐藏
        lift: 升旗
        putDown: 降旗
        any: 任意状态
        
        [*] --> hide
        
        hide --> lift : 收到升旗信号
        lift --> float : 升旗动画结束
        float --> putDown : 收到降旗信号
        putDown --> hide : 降旗动画结束
        
        any --> [*] : 本轮游戏结束
    }
```

## 类图

```mermaid
%%{init: {'theme':'neutral'}}%%
classDiagram
    direction BT
    
    class State {<<abstract>>}
    class FSM {<<abstract>>}
    State ..* FSM : Bridge Pattern
    
    class SpriteAnimState {<<abstract>>}
    class SpriteAnimFSM {<<abstract>>}
    SpriteAnimState --|> State
    SpriteAnimFSM --|> FSM
    
    class SpriteClip {<<abstract>>}
    class SpriteClipLoop
    class SpriteClipNonLoop
    SpriteClipLoop --|> SpriteClip
    SpriteClipNonLoop --|> SpriteClip
    
    SpriteClip ..* SpriteAnimState
    
    class CoverAnimState {<<abstract>>}
    class CoverAnimFSM
    CoverAnimState --|> SpriteAnimState
    CoverAnimFSM --|> SpriteAnimFSM
    
    class CoveredIdleState
    class RevealState
    class RevealedIdleState
    class PutCoverState
    
    CoveredIdleState --|> CoverAnimState
    RevealState --|> CoverAnimState
    RevealedIdleState --|> CoverAnimState
    PutCoverState --|> CoverAnimState
    
    
```